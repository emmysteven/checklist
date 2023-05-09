import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddItemComponent } from './add-item.component';
import { GetItemComponent } from './get-item.component';
import { AddSummaryComponent } from './add-summary.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GetSummaryComponent } from './get-summary.component';
import { SharedModule } from "@app/shared";


@NgModule({
  declarations: [AddItemComponent, GetItemComponent, AddSummaryComponent, GetSummaryComponent],
  imports: [CommonModule, ReactiveFormsModule, DashboardRoutingModule, FontAwesomeModule, SharedModule]
})
export class DashboardModule { }
