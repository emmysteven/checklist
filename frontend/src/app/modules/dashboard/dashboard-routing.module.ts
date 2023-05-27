import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/shared";
import { AddCheckComponent } from "./add-check.component";
import { GetCheckComponent } from "./get-check.component";
import { AddSummaryComponent } from "./add-summary.component";
import { GetSummaryComponent } from "./get-summary.component";
import { AuthGuard } from "@app/core/guards";
import { GetTodoComponent } from "./get-todo.component";
import { AddTodoComponent } from "./add-todo.component";

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
