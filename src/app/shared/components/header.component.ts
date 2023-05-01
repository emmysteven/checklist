import { Component, OnInit } from '@angular/core';

import { AuthService } from "@app/core/services";

@Component({
  selector: 'app-header',
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
      <div class="container">
        <a class="navbar-brand" routerLink="/">EOD Checklist</a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
            <li *ngIf="!isLoggedIn()" class="nav-item">
              <a class="nav-link" routerLink="/login">Login</a>
            </li>
            <li *ngIf="!isLoggedIn()" class="nav-item">
              <a class="nav-link" routerLink="/register">Register</a>
            </li>
            <li *ngIf="isLoggedIn()" class="nav-item me-2 dropdown">
              <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Reports</a>
              <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" routerLink="/dashboard/report/detail">Details</a></li>
                <li><a class="dropdown-item" routerLink="/dashboard">Summary</a></li>
<!--                <li><hr class="dropdown-divider" /></li>-->
<!--                <li><a class="dropdown-item" href="#">Something else here</a></li>-->
              </ul>
            </li>
            <li *ngIf="isLoggedIn()" class="nav-item me-2">
              <a class="nav-link" routerLink="/dashboard/add_item">add items</a>
            </li>
            <li *ngIf="isLoggedIn()" class="nav-item">
              <a class="nav-link" role="button" (click)="logout()">logout</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  `,
})
export class HeaderComponent implements OnInit {

  constructor(public authService: AuthService) {}
  isLoggedIn = ():boolean => this.authService.isLoggedIn();
  logout = ():void => this.authService.logout();
  ngOnInit(): void {}

}
