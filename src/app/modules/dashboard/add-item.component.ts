import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormArray, FormGroup, Validators } from "@angular/forms";
import { AlertService, ItemService } from "@app/core/services";
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
  todos: any;
  data: any;
  EodDate: string = '';

  preEodFields: FormArray<any> = new FormArray<any>([]);
  firstStageFields: FormArray<any> = new FormArray<any>([]);
  midEodFields: FormArray<any> = new FormArray<any>([]);
  lastStageFields: FormArray<any> = new FormArray<any>([]);
  postEodFields: FormArray<any> = new FormArray<any>([]);

  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private itemService: ItemService
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
    this.itemService.getTodos().subscribe({
      next: data => {
        this.data = data;

        this.data.forEach((todo: Todo) => {
            todo.startTime = "";
            todo.endTime = "";
            todo.status = "";
            todo.remark = "";
        })

        this.todos = this.data;
        this.createForm()
      },

      error: err => { console.log(err) }
    })
  }

  createForm() {
    this.todos.forEach((todo: Todo) => {
      if (todo.group.startsWith('PreEod')) {
        this.preEodFields.push(this.formBuilder.group({
          id: [todo.id],
          name: [todo.name],
          StartTime: ['', [Validators.required, Validators.maxLength(7)]],
          EndTime: ['', [Validators.required, Validators.maxLength(7)]],
          status: ['', [Validators.required, Validators.maxLength(7)]],
          remark: ['', [Validators.required, Validators.maxLength(7)]]
        }));
      }

      if (todo.group.startsWith('FirstStage')) {
        this.firstStageFields.push(this.formBuilder.group({
          id: [todo.id],
          name: [todo.name],
          StartTime: ['', [Validators.required, Validators.maxLength(7)]],
          EndTime: ['', [Validators.required, Validators.maxLength(7)]],
          status: ['', [Validators.required, Validators.maxLength(7)]],
          remark: ['', [Validators.required, Validators.maxLength(7)]]
        }));
      }

      if (todo.group.startsWith('MidEod')) {
        this.midEodFields.push(this.formBuilder.group({
          id: [todo.id],
          name: [todo.name],
          StartTime: ['', [Validators.required, Validators.maxLength(7)]],
          EndTime: ['', [Validators.required, Validators.maxLength(7)]],
          status: ['', [Validators.required, Validators.maxLength(7)]],
          remark: ['', [Validators.required, Validators.maxLength(7)]]
        }));
      }

      if (todo.group.startsWith('LastStage')) {
        this.lastStageFields.push(this.formBuilder.group({
          id: [todo.id],
          name: [todo.name],
          StartTime: ['', [Validators.required, Validators.maxLength(7)]],
          EndTime: ['', [Validators.required, Validators.maxLength(7)]],
          status: ['', [Validators.required, Validators.maxLength(7)]],
          remark: ['', [Validators.required, Validators.maxLength(7)]]
        }));
      }

      if (todo.group.startsWith('PostEod')) {
        this.postEodFields.push(this.formBuilder.group({
          id: [todo.id],
          name: [todo.name],
          StartTime: ['', [Validators.required, Validators.maxLength(7)]],
          status: ['', [Validators.required, Validators.maxLength(7)]],
          EndTime: ['', [Validators.required, Validators.maxLength(7)]],
          remark: ['', [Validators.required, Validators.maxLength(7)]]
        }));
      }
    });

    this.form = this.formBuilder.group({
      EodDate: ['', Validators.required],
      preEodFields: this.preEodFields,
      firstStageFields: this.firstStageFields,
      midEodFields: this.midEodFields,
      lastStageFields: this.lastStageFields,
      postEodFields: this.postEodFields
    });
  }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const data = this.form.value;

    const allFields = [
      ...data.preEodFields,
      ...data.firstStageFields,
      ...data.midEodFields,
      ...data.lastStageFields,
      ...data.postEodFields
    ];

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
