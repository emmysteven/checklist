import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService, ApiService } from "@app/core/services";
import { faUserPlus, faUserXmark } from '@fortawesome/free-solid-svg-icons';
import { first } from "rxjs/operators";
import { resetFormData } from "@app/core/utils";

@Component({
  selector: 'app-final-item',
  templateUrl: './add-summary.component.html',
  styles: [`
    .remove { color: red }
    .add { color: dodgerblue }
  `]
})

export class AddSummaryComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private alertService: AlertService,
    private apiService: ApiService
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
      txnCount: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
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

  get control() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) { return }

    const data = this.form.value;

    for (const prop in data) {
      if (Array.isArray(data[prop])) {
        data[prop] = data[prop].join(',')
      }
    }

    this.loading = true;

    this.apiService.addSummary(data)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.loading = false;
          resetFormData(this.form);
          this.alertService.success("Summary was added successfully");
          // setTimeout(() => { window.location.reload() }, 2000);
        },
        error: (error) => {
          this.loading = false;
          this.alertService.error("Something went wrong", { autoClose: false });
          console.log(error.error.errors);
        }
      });
  }

}
