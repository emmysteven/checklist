import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormArray, FormGroup, Validators } from "@angular/forms";
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
      return;
    }

    this.loading = true;
    // this.itemService.login(this.f['email'].value, this.f['password'].value)
    //   .pipe(first())
    //   .subscribe({
    //     next: (response) => {
    //       this.router.navigateByUrl(this.returnUrl);
    //     },
    //     error: (error) => {
    //       console.log(error);
    //       this.alertService.error(error);
    //       this.loading = false;
    //     }
    //   });
  }

}
