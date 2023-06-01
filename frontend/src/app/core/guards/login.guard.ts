import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from "@app/core/services";
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivate,
  UrlTree,
  Router
} from '@angular/router';


@Injectable({ providedIn: 'root' })
export class LoginGuard implements CanActivate {

  constructor(private userService: UserService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.userService.isLoggedIn()){
      this.router.navigateByUrl('/dashboard/checks', { skipLocationChange: true })
      return false
    }
    return true;
  }

}
