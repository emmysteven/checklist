import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/shared";
import { AddCheckComponent } from "./check/add-check.component";
import { GetCheckComponent } from "./check/get-check.component";
import { AddSummaryComponent } from "./summary/add-summary.component";
import { GetSummaryComponent } from "./summary/get-summary.component";
import { AuthGuard } from "@app/core/guards";
import { GetTodoComponent } from "./todo/get-todo.component";
import { AddTodoComponent } from "./todo/add-todo.component";
import { AddUserComponent, EditUserComponent, GetUserComponent } from "@app/modules/user";

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'users', component: GetUserComponent },
      { path: 'users/add', component: AddUserComponent },
      { path: 'users/:id', component: GetUserComponent },
      { path: 'users/edit/:id', component: EditUserComponent },
      { path: 'todos', component: GetTodoComponent },
      { path: 'todos/add', component: AddTodoComponent },
      { path: 'checks', component: GetCheckComponent },
      { path: 'checks/add', component: AddCheckComponent },
      { path: 'summary', component: GetSummaryComponent },
      { path: 'summary/add', component: AddSummaryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
