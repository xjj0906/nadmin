import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STReq, STRes } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { SysRoleEditComponent } from './edit/edit.component';
import { NzMessageService } from 'ng-zorro-antd';

const COMPONENT_NAME = '角色';

@Component({
  selector: 'app-sys-role',
  templateUrl: './role.component.html',
})
export class SysRoleComponent implements OnInit {
  url = `api/role`;
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
        title: '',
        ui: {
          placeholder: '名称',
        },
      },
    },
  };
  @ViewChild('st') st: STComponent;
  columns: STColumn[] = [
    { title: '名称', index: 'name' },
    { title: '描述', index: 'remark' },
    { title: '创建时间', index: 'createTime', type: 'date' },
    {
      title: '',
      buttons: [
        // { text: '查看', click: (item: any) => `/form/${item.id}` },
        {
          icon: 'edit',
          text: '编辑',
          type: 'static',
          component: SysRoleEditComponent,
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
            this.http.delete(`${this.url}/${record.id}`).subscribe((res: any) => {
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
      .createStatic(SysRoleEditComponent, {
        item: { id: 0 },
        title: `新建${COMPONENT_NAME}`,
      })
      .subscribe(() => this.st.reload());
  }
}
