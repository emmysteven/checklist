import { Component, OnInit } from '@angular/core';
import { AlertService, ItemService } from "@app/core/services";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import {Item, Todo} from "@app/core/models";

@Component({
  selector: 'app-get-item',
  templateUrl: './get-item.component.html',
  styles: [`
    .table { padding-bottom: 400px; }
  `]
})


export class GetItemComponent implements OnInit {

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
    private itemService: ItemService
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

    console.log(eodDate)

    if (this.form.invalid) { return console.log('Invalid Inputs') }

    this.loading = true;

    this.itemService.getItem(eodDate).subscribe({
      next: data => {
        this.loading = false
        this.items = data
        this.alertService.success(this.items)
      },
      error: err => {
        this.loading = false
        this.alertService.error(err)
        console.log(err)
      }
    });
  }

}
