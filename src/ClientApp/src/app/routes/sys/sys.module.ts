import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { SysRoutingModule } from './sys-routing.module';
import { SysUserComponent } from './user/user.component';
import { SysUserEditComponent } from './user/edit/edit.component';
import { SysRoleComponent } from './role/role.component';
import { SysRoleEditComponent } from './role/edit/edit.component';

const COMPONENTS = [
  SysUserComponent,
  SysRoleComponent];
const COMPONENTS_NOROUNT = [
  SysUserEditComponent,
  SysRoleEditComponent];

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
