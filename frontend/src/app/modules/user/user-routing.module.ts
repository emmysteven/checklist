import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./login.component";
import { LoginGuard } from "@app/core/guards/login.guard";

const routes: Routes = [
  { path: '', component: LoginComponent, canActivate: [LoginGuard] },
  // { path: 'user/add', component: AddUserComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class UserRoutingModule { }
