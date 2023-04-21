import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "@env/environment";
import { ITodo } from "@app/core/models/todo";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTodos(): Observable<ITodo> {
    return this.http.get<ITodo>(`${this.baseUrl}api/todo`, httpOptions)
      .pipe(map((response: any) => {
        const data = response.data
        return data;
      }))
  }

  addItem(item: any) {
    return this.http.post(`${this.baseUrl}api/item`, item);
  }
}
