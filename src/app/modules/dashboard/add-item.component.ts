import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormArray, FormGroup, Validators } from "@angular/forms";
import { AlertService, ApiService } from "@app/core/services";
import { first } from "rxjs/operators";
import { Todo } from "@app/core/models";

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styles: [`
    form { padding-bottom: 100px; }
    table .form-control { width: 130px; }
    .form-group:nth-child(1) {
      display: flex;
      flex-wrap: wrap;
    }
  `]
})

export class AddItemComponent implements OnInit {

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
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard/final_item';
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
      name: [todo.name],
      StartTime: ['', [Validators.required, Validators.maxLength(7)]],
      EndTime: ['', [Validators.required, Validators.maxLength(7)]],
      remark: ['', [Validators.required, Validators.maxLength(100)]]
    })
  }

  createForm() {
    this.todos.forEach((todo: Todo) => {
      if (todo.group.startsWith('PreEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.group.startsWith('FirstStage')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.group.startsWith('MidEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.group.startsWith('LastStage')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }

      if (todo.group.startsWith('PostEod')) {
        this.Fields.push(this.createFieldsForGroup(todo));
      }
    });

    this.form = this.formBuilder.group({
      EodDate: ['', Validators.required],
      allFields: this.Fields
    });
  }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const data = this.form.value;
    const Fields = [...data.Fields];

    //sort the objects in the array in ascending order using the ids of the objects
    const sortedFields = Fields.sort((a, b) => a.id - b.id);

    sortedFields.forEach((obj: { EodDate: string; }) => {
      obj.EodDate = this.form.value.EodDate;
    });

    const body = {
      items: sortedFields
    };

    console.log(body);
    if (this.form.invalid) { return console.log('Invalid Inputs') }

    this.loading = true;

    this.apiService.addItem(body)
      .pipe(first())
      .subscribe({
        next: (response) => {
          console.log(response);
          this.alertService.success('Item added successfully');
          this.loading = false;
          this.router.navigateByUrl(this.returnUrl);
        },
        error: (error) => {
          console.log(error);
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
