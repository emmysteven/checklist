import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeaderComponent, FooterComponent } from './components';


@NgModule({
  declarations: [FooterComponent, HeaderComponent],
  exports: [HeaderComponent, FooterComponent],
  imports: [CommonModule, RouterModule]
})

export class SharedModule { }
