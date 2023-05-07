import { Component, OnInit } from '@angular/core';
import { AlertService, ApiService } from "@app/core/services";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-summary',
  templateUrl: './get-summary.component.html',
  styles: [
  ]
})
export class GetSummaryComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  data: any;
  summary: any;
  eodDate: string = '';
  faMagnifyingGlass = faMagnifyingGlass;

  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      eodDate: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const eodDate = this.form.value.eodDate;

    if (this.form.invalid) { return console.log('Invalid Inputs') }

    this.loading = true;

    this.apiService.getSummary(eodDate).subscribe({
      next: data => {
        this.loading = false
        this.summary = data
        console.log(this.summary)
        this.alertService.success(this.summary)
      },
      error: err => {
        this.loading = false
        this.alertService.error(err)
        console.log(err)
      }
    });
  }
}
