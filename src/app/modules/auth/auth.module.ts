import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { AuthRoutingModule } from './auth-routing.module';
import { SignupComponent } from './signup.component';
import { LoginComponent } from './login.component';
import { LayoutComponent } from './layout.component';


@NgModule({
  declarations: [SignupComponent, LoginComponent, LayoutComponent],
  imports: [CommonModule, AuthRoutingModule, ReactiveFormsModule]
})
export class AuthModule { }
