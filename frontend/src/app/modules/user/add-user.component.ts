import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators} from "@angular/forms";
import { first } from "rxjs/operators";
import { UserService, AlertService } from "@app/core/services";
import { resetFormData } from "@app/core/utils";

@Component({
  selector: 'app-signup',
  templateUrl: './add-user.component.html',
  styles: []
})
export class AddUserComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      userName: ['', Validators.required],
      role: ['', Validators.required]
    });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) { return }

    this.loading = true;
    this.userService.addUser(this.form.value).pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          this.alertService.success("User was added successfully");
          resetFormData(this.form);
          console.log(data);
        },
        (error) => {
          this.alertService.error(error.message);
          console.log(error.message);
          this.loading = false;
        });
  }

}
