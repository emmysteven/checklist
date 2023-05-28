import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared';
import { AppComponent } from './app.component';
import { UserModule } from "./modules/user";
import { DashboardModule } from "./modules/dashboard";
import { TokenInterceptor } from "./core/interceptors";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    UserModule,
    SharedModule,
    BrowserModule,
    DashboardModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
