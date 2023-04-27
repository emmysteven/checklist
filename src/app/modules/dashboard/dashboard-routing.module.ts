import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from "@app/modules/auth/layout.component";
import { AddItemComponent } from "./add-item.component";
import { GetItemComponent } from "./get-item.component";

const routes: Routes = [
  {
    path: 'dashboard', component: LayoutComponent,
    children: [
      { path: 'add_item', component: AddItemComponent },
      { path: 'report/detail', component: GetItemComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
