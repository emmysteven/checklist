import { Component, OnInit } from '@angular/core';
import { AlertService, ApiService } from "@app/core/services";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import {Item, Todo} from "@app/core/models";

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
  items: any;
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

  get control() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    const eodDate = this.form.value.eodDate;

    if (this.form.invalid) { return console.log('Invalid Inputs') }

    this.loading = true;

    this.apiService.getItem(eodDate).subscribe({
      next: data => {
        this.loading = false
        this.items = data
      },
      error: err => {
        this.loading = false
        this.alertService.error("Something went wrong, please try again" + err)
        console.log(err)
      }
    });
  }

}
