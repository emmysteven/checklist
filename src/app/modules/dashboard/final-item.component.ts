import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService, ItemService } from "@app/core/services";
import { faUserPlus, faUserXmark } from '@fortawesome/free-solid-svg-icons';
import { first } from "rxjs/operators";

@Component({
  selector: 'app-final-item',
  templateUrl: './final-item.component.html',
  styles: [`
    .remove { color: red }
    .add { color: dodgerblue }
  `]
})

export class FinalItemComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private alertService: AlertService,
    private itemService: ItemService
  ) { }

  faUserPlus = faUserPlus;
  faUserXmark = faUserXmark;

  form: FormGroup = new FormGroup({});
  data: any;
  makers = this.fb.array([this.fb.control('', Validators.required)]);
  checkers = this.fb.array([this.fb.control('', Validators.required)]);
  dbas = this.fb.array([this.fb.control('', Validators.required)]);

  loading: boolean = false;
  submitted: boolean = false;

  ngOnInit(): void {
    this.form = this.fb.group({
      makers: this.makers,
      checkers: this.checkers,
      dbas: this.dbas,
      startTime: ['', Validators.required],
      endTime:  ['', Validators.required],
      duration: ['', Validators.required],
      txnCount: ['', Validators.required],
      eodDate: ['', Validators.required]
    });
  }

  addMaker(): void {
    this.makers.push(this.fb.control('', Validators.required));
  }

  addChecker(): void {
    this.checkers.push(this.fb.control('', Validators.required));
  }

  addDba(): void {
    this.dbas.push(this.fb.control('', Validators.required));
  }

  removeMaker(index: number) {
    this.makers.removeAt(index);
  }

  removeChecker(index: number) {
    this.checkers.removeAt(index);
  }

  removeDba(index: number) {
    this.dbas.removeAt(index);
  }


  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) {
      console.log("form is invalid" + this.form.invalid)
    }

    const data = this.form.value;

    for (const prop in data) {
      if (Array.isArray(data[prop])) {
        data[prop] = data[prop].join(',')
      }
    }

    console.log(data);

    this.loading = true;
    this.itemService.addFinalItem(data)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.loading = false;
          console.log(response)
        },
        error: (error) => {
          console.log(error);
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

}
