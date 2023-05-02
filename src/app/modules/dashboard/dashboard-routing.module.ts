import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/modules/auth/layout.component";
import { AddItemComponent } from "./add-item.component";
import { GetItemComponent } from "./get-item.component";
import { FinalItemComponent } from "./final-item.component";
import { SummaryComponent } from "@app/modules/dashboard/summary.component";
import { AuthGuard } from "@app/core/guards";

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'add_item', component: AddItemComponent },
      { path: 'final_item', component: FinalItemComponent },
      { path: 'report/detail', component: GetItemComponent },
      { path: 'report/summary', component: SummaryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
