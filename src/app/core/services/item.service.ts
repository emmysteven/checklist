import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "@env/environment";
import {Item, Todo} from "@app/core/models";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo> {
    return this.http.get<Todo>(`${this.baseUrl}api/todo`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
      }))
  }

  addItem(item: any) {
    return this.http.post(`${this.baseUrl}api/item`, item);
  }

  getItem(eodDate: string): Observable<Item> {
    return this.http.get<Item>(`${this.baseUrl}api/item?eodDate=${eodDate}`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
    }))
  }

  getSummary(eodDate: string): Observable<Item> {
    return this.http.get<Item>(`${this.baseUrl}api/summary?eodDate=${eodDate}`, httpOptions)
      .pipe(map((response: any) => {
        return response.data;
      }))
  }
}
