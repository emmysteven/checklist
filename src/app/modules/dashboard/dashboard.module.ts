import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddItemComponent } from './add-item.component';
import { GetItemComponent } from './get-item.component';
import { FinalItemComponent } from './final-item.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SummaryComponent } from './summary.component';


@NgModule({
  declarations: [AddItemComponent, GetItemComponent, FinalItemComponent, SummaryComponent],
  imports: [CommonModule, ReactiveFormsModule, DashboardRoutingModule, FontAwesomeModule]
})
export class DashboardModule { }
