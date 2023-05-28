import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";

import { User } from "../models/user";

import { map } from "rxjs/operators";
import { environment } from "@env/environment";

@Injectable({ providedIn: 'root' })
export class AuthService {

  newUser: User = {
    id: '',
    fullName: '',
    userName: '',
    role: ''
  }
  baseUrl: string = environment.baseUrl;
  helper = new JwtHelperService();

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>

  constructor(private router: Router, private http: HttpClient) {
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(username: string, password: string): Observable<User> {
    return this.http.post<User>(`${this.baseUrl}api/user/login`, { username, password })
      .pipe(map((response: any) => {
        const token = response.token;
        if (token) {
          localStorage.setItem('token', token);
          const decodedToken = this.helper.decodeToken(token);

          this.newUser = {
            id: decodedToken.Id,
            fullName: decodedToken.FullName,
            userName: decodedToken.UserName,
            role: decodedToken.Role
          }

          console.log(this.newUser);
          this.userSubject.next(response);
        }

        return response;
      }))
  }

  register(user: User) {
    return this.http.post(`${this.baseUrl}user/register`, user);
  }

  getAll() {
    return this.http.get<User[]>(`${this.baseUrl}users`);
  }

  getById(id: string) {
    return this.http.get<User>(`${this.baseUrl}user/${id}`);
  }

  update(id: string, params: object) {
    return this.http.put(`${this.baseUrl}user/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('token', JSON.stringify(user));

          // publish updated user to subscribers
          this.userSubject.next(user);
        }
        return x;
      }));
  }

  delete(id: string) {
    return this.http.delete(`${this.baseUrl}user/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          this.logout();
        }
        return x;
      }));
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token') || '';
    return !this.helper.isTokenExpired(token);
  }

  logout(): void {
    this.newUser = {
      id: '',
      fullName: '',
      userName: '',
      role: ''
    }
    localStorage.removeItem('token');
    this.userSubject.next(this.newUser);
    this.router.navigate(['/']);
  }

}

