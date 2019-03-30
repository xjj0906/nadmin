import { Component, OnInit } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-sys-role-edit',
  templateUrl: './edit.component.html',
})
export class SysRoleEditComponent implements OnInit {
  url = `api/role`;
  title: string; // Modal标题
  record: any = {}; // 父窗体传进来的记录
  item: any; // 新增：构造的 id = 0 空记录，编辑：服务器请求回来的记录，相对于 FormGroup 中的数据，此为未被修改的数据
  validateForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private msg: NzMessageService,
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
  ) {
    this.validateForm = this.fb.group({
      name: [null, [Validators.required]],
      remark: [null],
    });
  }

  submitForm(): void {
    for (const i in this.validateForm.controls) {
      if (this.validateForm.controls.hasOwnProperty(i)) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }
    }
    if (!this.validateForm.valid) return;

    // this.msg.success(JSON.stringify(this.validateForm.value));
    if (this.record.id > 0) {
      this.Update(this.validateForm.value);
    } else {
      this.Insert(this.validateForm.value);
    }

    // this.msgSrv.success('保存成功');
    // this.modal.close(true);
  }

  ngOnInit(): void {
    if (this.record.id > 0)
      this.http.get(`${this.url}/${this.record.id}`).subscribe((res: any) => {
        this.item = res.result;
        this.validateForm.controls.name.setValue(this.item.name);
        this.validateForm.controls.remark.setValue(this.item.remark);
      });
  }

  Insert(value: any) {
    this.http.post(this.url, value).subscribe((res: any) => {
      if (res.status === 0) {
        this.msgSrv.success('保存成功');
        this.modal.close(true);
      } else {
        this.msg.warning(res.msg);
      }
    });
  }

  Update(value: any) {
    this.http.put(`${this.url}/${this.record.id}`, value).subscribe((res: any) => {
      if (res.status === 0) {
        this.msgSrv.success('保存成功');
        this.modal.close(true);
      } else {
        this.msg.warning(res.msg);
      }
    });
  }

  close() {
    this.modal.destroy();
  }
}
