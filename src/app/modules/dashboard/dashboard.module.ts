import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddItemComponent } from './add-item.component';
import { GetItemComponent } from './get-item.component';


@NgModule({
  declarations: [AddItemComponent, GetItemComponent],
  imports: [CommonModule, ReactiveFormsModule, DashboardRoutingModule]
})
export class DashboardModule { }
