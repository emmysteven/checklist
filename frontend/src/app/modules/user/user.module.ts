import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { UserRoutingModule } from './user-routing.module';
import { AddUserComponent } from './add-user.component';
import { LoginComponent } from './login.component';
import { LayoutComponent } from '@app/shared';


@NgModule({
  declarations: [AddUserComponent, LoginComponent, LayoutComponent],
  imports: [CommonModule, UserRoutingModule, ReactiveFormsModule]
})
export class UserModule { }
