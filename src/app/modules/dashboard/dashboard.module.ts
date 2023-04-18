import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddItemComponent } from './add-item.component';


@NgModule({
  declarations: [AddItemComponent],
  imports: [CommonModule, DashboardRoutingModule]
})
export class DashboardModule { }
