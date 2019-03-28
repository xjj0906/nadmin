import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { SysRoutingModule } from './sys-routing.module';
import { SysUserComponent } from './user/user.component';
import { SysUserUserEditComponent } from './user/user-edit/user-edit.component';

const COMPONENTS = [
  SysUserComponent];
const COMPONENTS_NOROUNT = [
  SysUserUserEditComponent];

@NgModule({
  imports: [
    SharedModule,
    SysRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class SysModule { }
