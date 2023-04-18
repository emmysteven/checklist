import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService, ItemService } from "@app/core/services";
import { first } from "rxjs/operators";


@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styles: [
  ]
})
export class AddItemComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  todos: any;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({});

    this.itemService.getTodos().subscribe({
      next: data => {
        this.todos = data;
      },
      error: err => {
        if (err.error) {
          try {
            const res = JSON.parse(err.error);
            this.todos = res.message;
          } catch {
            this.todos = `Error with status: ${err.status} - ${err.statusText}`;
          }
        } else {
          this.todos = `Error with status: ${err.status}`;
        }
      }
    });

    console.log(this.todos);
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) { return }

    this.loading = true;
    this.itemService.addItem(this.f)
      .pipe(first())
      .subscribe({
        next: (response) => {
          console.log(response);
        },
        error: (error) => {
          console.log(error);
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }


}
