import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { UserRoutingModule } from './user-routing.module';
import { AddUserComponent } from './add-user.component';
import { LoginComponent } from './login.component';
import { LayoutComponent, SharedModule } from '@app/shared';
import { GetUserComponent } from './get-user.component';
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { EditUserComponent } from './edit-user.component';


@NgModule({
  declarations: [AddUserComponent, LoginComponent, LayoutComponent, GetUserComponent, EditUserComponent],
  imports: [CommonModule, UserRoutingModule, ReactiveFormsModule, SharedModule, FontAwesomeModule]
})
export class UserModule { }
