import { Injectable } from '@angular/core';
import { AuthService } from "@app/core/services";
import { Observable } from 'rxjs';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivate,
  UrlTree
} from '@angular/router';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.authService.isLoggedIn()){
      return true;
    }
    this.authService.logout();
    return false;
  }
}
