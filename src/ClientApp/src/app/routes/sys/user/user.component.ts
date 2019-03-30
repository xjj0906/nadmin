import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STColumnTag, STReq, STRes } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysUserEditComponent } from './edit/edit.component';
import { NzMessageService } from 'ng-zorro-antd';

const COMPONENT_NAME = '用户';

const STATUS_TAG: STColumnTag = {
  0: { text: '正常', color: 'green' },
  1: { text: '冻结', color: 'red' },
};

@Component({
  selector: 'app-sys-user',
  templateUrl: './user.component.html',
})
export class SysUserComponent implements OnInit {
  url = `api/user`;
  req: STReq = {
    reName: {
      pi: 'pageIndex',
      ps: 'pageSize',
    },
  };
  res: STRes = {
    reName: {
      list: 'result',
      total: 'totalCount',
    },
  };
  searchSchema: SFSchema = {
    properties: {
      keyword: {
        type: 'string',
        title: '用户名',
      },
    },
  };
  @ViewChild('st') st: STComponent;
  columns: STColumn[] = [
    { title: '用户名', index: 'userName' },
    { title: '头像', index: 'avatar', type: 'img', width: '50px' },
    { title: '邮箱', index: 'email' },
    { title: '手机', index: 'phoneNumber' },
    { title: '状态', index: 'status', type: 'tag', tag: STATUS_TAG },
    { title: '备注', index: 'remark' },
    { title: '创建时间', index: 'createTime', type: 'date' },
    {
      title: '',
      buttons: [
        // { text: '查看', click: (item: any) => `/form/${item.id}` },
        {
          icon: 'edit',
          text: '编辑',
          type: 'static',
          component: SysUserEditComponent,
          paramName: 'record', // Modal中当前行的参数名
          params: (r: any) => {
            return { title: `编辑${COMPONENT_NAME}` };
          },
          click: 'reload',
        },
        {
          icon: 'delete',
          text: '删除',
          type: 'del',
          click: (record, modal, comp) => {
            this.http.delete(`api/user/${record.id}`).subscribe((res: any) => {
              if (res.status !== 0) {
                this.message.warning(res.msg);
              } else {
                comp.removeRow(record);
                this.message.success(
                  `成功删除${COMPONENT_NAME}【${record.userName}】`,
                );
              }
            });
          },
          iif: (item: any) => item.id % 2 === 0,
        },
      ],
    },
  ];

  constructor(
    private http: _HttpClient,
    private modal: ModalHelper,
    private message: NzMessageService,
  ) {}

  ngOnInit() {}

  add() {
    this.modal
      .createStatic(SysUserEditComponent, {
        item: { id: 0 },
        title: `新建${COMPONENT_NAME}`,
      })
      .subscribe(() => this.st.reload());
  }
}
