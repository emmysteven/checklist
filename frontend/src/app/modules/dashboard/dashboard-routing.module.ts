import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/modules/auth/layout.component";
import { AddCheckComponent } from "./add-check.component";
import { GetCheckComponent } from "./get-check.component";
import { AddSummaryComponent } from "./add-summary.component";
import { GetSummaryComponent } from "@app/modules/dashboard/get-summary.component";
import { AuthGuard } from "@app/core/guards";

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
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
