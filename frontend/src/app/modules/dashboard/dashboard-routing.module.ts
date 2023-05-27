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

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'todos/get_todos', component: GetTodoComponent },
      { path: 'todos/add_todos', component: AddTodoComponent },
      { path: 'checks/add_checks', component: AddCheckComponent },
      { path: 'checks/add_summary', component: AddSummaryComponent },
      { path: 'report/get_check', component: GetCheckComponent },
      { path: 'report/get_summary', component: GetSummaryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
