import { Component, OnInit } from '@angular/core';
import { AlertService, ApiService } from "@app/core/services";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { faCheckDouble ,faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { duration } from "@app/core/utils";

@Component({
  selector: 'app-get-item',
  templateUrl: './get-check.component.html',
  styles: [`
    .table { padding-bottom: 400px; }
  `]
})


export class GetCheckComponent implements OnInit {

  form: FormGroup = new FormGroup({});

  data: any;
  checks: any;
  eodDate: string = '';

  faMagnifyingGlass = faMagnifyingGlass;
  faCheckDouble = faCheckDouble;

  loading = false;
  submitted = false;

  progress = false;
  clicked = false;

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

  get control() { return this.form.controls; }

  authorize(): void {
    this.clicked = true;
    this.alertService.clear();

    if (this.form.invalid) { return }
    this.progress = true;
    console.log(this.form.value)

    this.apiService.authCheck(this.form.value).subscribe({
      next: data => {
        this.progress = false
        this.checks = duration(data)
        console.log(data);
      },
      error: err => {
        this.progress = false
        this.alertService.error("Something went wrong, please try again" + err)
        console.log(err)
      }
    });
  }

  search(): void {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) { return }
    this.loading = true;

    this.apiService.getCheck(this.control['eodDate'].value).subscribe({
      next: data => {
        this.loading = false
        this.checks = duration(data)
        console.log(data);
      },
      error: err => {
        this.loading = false
        this.alertService.error("Something went wrong, please try again" + err)
        console.log(err)
      }
    });
  }

}
