import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/modules/auth/layout.component";
import { AddItemComponent } from "./add-item.component";
import { GetItemComponent } from "./get-item.component";
import { AddSummaryComponent } from "./add-summary.component";
import { GetSummaryComponent } from "@app/modules/dashboard/get-summary.component";
import { AuthGuard } from "@app/core/guards";

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'checks/add_checks', component: AddItemComponent },
      { path: 'checks/add_summary', component: AddSummaryComponent },
      { path: 'report/detail', component: GetItemComponent },
      { path: 'report/get_summary', component: GetSummaryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
