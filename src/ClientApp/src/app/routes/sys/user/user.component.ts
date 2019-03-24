import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STColumnTag, STReq, STRes } from '@delon/abc';
import { SFSchema } from '@delon/form';

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
      pi: "pageIndex",
      ps: "pageSize",
    }
  };
  res: STRes = {
    reName: {
      list: "result",
      total: "totalCount",
    }
  };
  searchSchema: SFSchema = {
    properties: {
      keyword: {
        type: 'string',
        title: '用户名'
      }
    }
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
        // { text: '编辑', type: 'static', component: FormEditComponent, click: 'reload' },
      ]
    }
  ];

  constructor(private http: _HttpClient, private modal: ModalHelper) { }

  ngOnInit() { }

  add() {
    // this.modal
    //   .createStatic(FormEditComponent, { i: { id: 0 } })
    //   .subscribe(() => this.st.reload());
  }

}
