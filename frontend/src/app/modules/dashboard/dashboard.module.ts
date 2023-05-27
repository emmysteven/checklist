import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddCheckComponent } from './add-check.component';
import { GetCheckComponent } from './get-check.component';
import { AddSummaryComponent } from './add-summary.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GetSummaryComponent } from './get-summary.component';
import { SharedModule } from "@app/shared";
import { AddTodoComponent } from './add-todo.component';
import { GetTodoComponent } from './get-todo.component';


@NgModule({
  declarations: [
    AddCheckComponent,
    GetCheckComponent,
    AddSummaryComponent,
    GetSummaryComponent,
    AddTodoComponent,
    GetTodoComponent
  ],

  imports: [
    CommonModule,
    ReactiveFormsModule,
    DashboardRoutingModule,
    FontAwesomeModule,
    SharedModule
  ]
})

export class DashboardModule { }
