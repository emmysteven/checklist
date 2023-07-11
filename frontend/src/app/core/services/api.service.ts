import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "@env/environment";
import { Todo, Summary, CheckVm } from "@app/core/models";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo> {
    return this.http.get<Todo>(`${this.baseUrl}api/todo`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
      }))
  }

  addCheck(check: any) {
    return this.http.post(`${this.baseUrl}api/check`, check);
  }

  addSummary(summary: any) {
    return this.http.post(`${this.baseUrl}api/summary`, summary);
  }

  getCheck(eodDate: string): Observable<CheckVm[]> {
    return this.http.get<CheckVm[]>(`${this.baseUrl}api/check?eodDate=${eodDate}`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
    }))
  }

  getSummary(eodDate: string): Observable<Summary> {
    return this.http.get<Summary>(`${this.baseUrl}api/summary?eodDate=${eodDate}`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
      }))
  }

  getSummaryById(id: number): Observable<Summary> {
    return this.http.get<Summary>(`${this.baseUrl}api/summary/${id}`, httpOptions)
      .pipe(map((response: any) => {
        console.log(response);
        return response.data;
      }))
  }

  authCheck(eodDate: string): Observable<CheckVm[]> {
    return this.http.patch<CheckVm[]>(`${this.baseUrl}api/check`, eodDate)
      .pipe(map((response: any) => {
        return response.data;
    }))
  }

  authSummary(eodDate: string): Observable<Summary> {
    return this.http.patch<Summary>(`${this.baseUrl}api/summary`, eodDate)
      .pipe(map((response: any) => {
        return response.data;
      }))
  }

}
