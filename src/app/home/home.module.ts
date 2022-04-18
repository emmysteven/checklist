import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { AboutComponent } from './about.component';
import { ContactComponent } from './contact.component';


@NgModule({
  declarations: [
    AboutComponent,
    ContactComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
