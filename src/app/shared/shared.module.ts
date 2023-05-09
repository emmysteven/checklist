import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeaderComponent, FooterComponent, AlertComponent } from './components';


@NgModule({
  declarations: [FooterComponent, HeaderComponent, AlertComponent],
  exports: [HeaderComponent, FooterComponent, AlertComponent],
  imports: [CommonModule, RouterModule]
})

export class SharedModule { }
