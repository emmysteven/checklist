import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared';
import { AppComponent } from './app.component';
import { AuthModule } from "./modules/auth";
import { DashboardModule } from "./modules/dashboard";
import { tokenInterceptorProvider } from "./core/interceptors";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AuthModule,
    SharedModule,
    BrowserModule,
    DashboardModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [tokenInterceptorProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
