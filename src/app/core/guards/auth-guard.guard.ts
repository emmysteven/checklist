import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from 'rxjs';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivate,
  UrlTree,
  Router
} from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardGuard implements CanActivate {

  constructor(private jwtHelper: JwtHelperService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const token = localStorage.getItem("token");
    console.log(token)

    //Check if the token is expired or not
    // and if token is expired then redirect to login page and return false
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    this.router.navigate(["login"]);
    return false;
  }
}
