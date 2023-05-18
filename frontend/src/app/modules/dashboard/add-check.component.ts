import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormArray, FormGroup, Validators } from "@angular/forms";
import { AlertService, ApiService } from "@app/core/services";
import { first } from "rxjs/operators";
import { Todo } from "@app/core/models";

@Component({
  selector: 'app-add-item',
  templateUrl: './add-check.component.html',
  styles: [`
    form { padding-bottom: 100px; }
    table .form-control { width: 130px; }
    .form-group:nth-child(1) {
      display: flex;
      flex-wrap: wrap;
    }
  `]
})

export class AddCheckComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  Fields: FormArray<any> = new FormArray<any>([]);

  todos: any;
  data: any;
  EodDate: string = '';
  returnUrl: string;

  loading = false;
  submitted = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private apiService: ApiService
  ) {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard/checks/add_summary';
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      EodDate: ['', [Validators.required]],
    });
    this.getTodos()
  }

  getTodos(){
    this.apiService.getTodos().subscribe({
      next: data => {
        this.data = data;

        console.log(data)

        this.data.forEach((todo: Todo) => {
            todo.startTime = "";
            todo.endTime = "";
            todo.remark = "";
        })

        this.todos = this.data;
        this.createForm()
      },

      error: err => { console.log(err) }
    })
  }

  createFieldsForGroup(todo: Todo) {
    return this.formBuilder.group({
      id: [todo.id],
      checkName: [todo.todoName],
      StartTime: ['', [Validators.required, Validators.maxLength(7)]],
      EndTime: ['', [Validators.required, Validators.maxLength(7)]],
      remark: ['', [Validators.maxLength(100)]]
    })
  }

  createForm() {
    this.todos.forEach((todo: Todo) => {
      if (todo.todoGroup.startsWith('PreEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.todoGroup.startsWith('FirstStage')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.todoGroup.startsWith('MidEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.todoGroup.startsWith('LastStage')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.todoGroup.startsWith('PostEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }
    });

    this.form = this.formBuilder.group({
      EodDate: ['', Validators.required],
      allFields: this.Fields
    });
  }

  get control() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const Fields = [...this.form.value.allFields]

    Fields.forEach((obj: { EodDate: string; }) => {
      obj.EodDate = this.form.value.EodDate
    });

    const body = { checks: Fields }

    console.log(body);
    if (this.form.invalid) {
      this.alertService.error("Some fields are empty: ", { autoClose: false });
      return;
    }

    this.loading = true;

    this.apiService.addCheck(body)
      .pipe(first())
      .subscribe({
        next: (response) => {
          console.log(response);
          this.alertService.success('Checks were added successfully', { autoClose: false });
          this.loading = false;
          this.router.navigateByUrl(this.returnUrl);
        },
        error: (error) => {
          console.log(error);
          this.alertService.error("Something went wrong, please try again" + error)
          this.loading = false;
        }
      });
  }
}
