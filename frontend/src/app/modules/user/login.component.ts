import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { UserService, AlertService } from "@app/core/services";
import { first } from "rxjs/operators";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [`
    .mt-5 {
      padding-top: 4rem;
    }
  `]
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: UserService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid) { return }

    this.loading = true;
    this.authService.login(this.f['username'].value, this.f['password'].value)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/dashboard/checks').then(() => window.location.reload());
        },
        error: (error) => {
          console.log(error);
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

}
