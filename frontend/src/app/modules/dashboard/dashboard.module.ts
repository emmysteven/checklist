import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AddCheckComponent } from './check/add-check.component';
import { GetCheckComponent } from './check/get-check.component';
import { AddSummaryComponent } from './summary/add-summary.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GetSummaryComponent } from './summary/get-summary.component';
import { SharedModule } from "@app/shared";
import { AddTodoComponent } from './todo/add-todo.component';
import { GetTodoComponent } from './todo/get-todo.component';


@NgModule({
  declarations: [
    AddCheckComponent,
    GetCheckComponent,
    AddSummaryComponent,
    GetSummaryComponent,
    AddTodoComponent,
    GetTodoComponent,
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
