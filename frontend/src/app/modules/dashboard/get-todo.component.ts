import { Component } from '@angular/core';
import {AlertService, ApiService} from "@app/core/services";

@Component({
  selector: 'app-get-todo',
  templateUrl: './get-todo.component.html',
  styles: [
  ]
})

export class GetTodoComponent {
  todos: any;

  constructor(
    private alertService: AlertService,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.apiService.getTodos().subscribe({
      next: data => {
        this.todos = data
      },
      error: err => {
        this.alertService.error("Something went wrong, please try again" + err)
        console.log(err)
      }
    });
  }
}
