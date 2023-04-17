import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from "@app/modules/auth";
import { HeaderComponent, FooterComponent } from './components';


@NgModule({
  declarations: [FooterComponent, HeaderComponent],
  exports: [HeaderComponent, FooterComponent],
  imports: [CommonModule, AuthRoutingModule]
})

export class SharedModule { }
