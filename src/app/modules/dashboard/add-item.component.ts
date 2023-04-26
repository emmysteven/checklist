import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, FormGroup, Validators } from "@angular/forms";
import { AlertService, ItemService } from "@app/core/services";
import { first } from "rxjs/operators";
import { Todo } from "@app/core/models";

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styles: [`
    form { padding-bottom: 100px; }
    .form-control { width: 130px; }
    .form-group:nth-child(1) {
      display: flex;
      flex-wrap: wrap;
    }
  `]
})

export class AddItemComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  todos: any;
  data: any;
  EodDate: string = '';

  timeFields: FormArray<any> = new FormArray<any>([]);
  startEndFields: FormArray<any> = new FormArray<any>([]);

  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      EodDate: ['', [Validators.required]],
      timeFields: this.formBuilder.array([
        this.formBuilder.group({
          id: [],
          name: [''],
          startTime: ['']
        })
      ]),
      startEndFields: this.formBuilder.array([
        this.formBuilder.group({
          id: [],
          name: [''],
          startTime: [''],
          endTime: ['']
        })
      ])
    });

    this.itemService.getTodos().subscribe({
      next: data => {
        this.data = data;

        this.data.forEach((todo: Todo) => {
          if (todo.name.startsWith('Submit')) {
            todo.startTime = "";
            todo.endTime = "";
          } else {
            todo.startTime = "";
          }
        });

        this.todos = this.data;

        console.log(this.todos);

        this.todos.forEach((todo: Todo) => {
          if (todo.name.startsWith('Submit')) {
            this.startEndFields.push(this.formBuilder.group({
              id: [todo.id],
              name: [todo.name],
              StartTime: ['', [Validators.required, Validators.maxLength(7)]],
              EndTime: ['', [Validators.required, Validators.maxLength(7)]]
            }));
          } else {
            this.timeFields.push(this.formBuilder.group({
              id: [todo.id],
              name: [todo.name],
              StartTime: ['', [Validators.required, Validators.maxLength(7)]]
            }));
          }
        });

        this.form = this.formBuilder.group({
          EodDate: ['', Validators.required],
          timeFields: this.timeFields,
          startEndFields: this.startEndFields
        });
      },
      error: err => {
        console.log(err);
      }
    });
  }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const data = this.form.value;
    const allFields = [...data.startEndFields, ...data.timeFields];

    //sort the objects in the array in ascending order using the ids of the objects
    const sortedFields = allFields.sort((a, b) => a.id - b.id);

    sortedFields.forEach((obj: { EodDate: string; }) => {
      obj.EodDate = this.form.value.EodDate;
    });

    const body = {
      items: sortedFields
    };

    console.log(body);
    if (this.form.invalid) { return console.log('Invalid Inputs') }

    this.loading = true;

    this.itemService.addItem(body)
      .pipe(first())
      .subscribe({
        next: (response) => {
          console.log(response);
          this.alertService.success('Item added successfully', { keepAfterRouteChange: true });
          this.loading = false;
        },
        error: (error) => {
          console.log(error);
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
