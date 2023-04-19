import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "@env/environment";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTodos() {
    return this.http.get<any>(`${this.baseUrl}api/todo`, httpOptions);
  }

  addItem(item: any) {
    return this.http.post(`${this.baseUrl}api/item`, item);
  }
}
