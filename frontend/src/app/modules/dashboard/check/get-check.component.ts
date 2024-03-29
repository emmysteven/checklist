import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService, ApiService, UserService } from "@app/core/services";
import { duration } from "@app/core/utils";
import { faCheckDouble, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';

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
  userRole: string | null = '';

  faMagnifyingGlass = faMagnifyingGlass;
  faCheckDouble = faCheckDouble;

  loading = false;
  submitted = false;

  buttonDisabled = false;
  progress = false;
  clicked = false;

  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private userService: UserService,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      eodDate: ['', Validators.required]
    });
    this.userRole = this.userService.getUserRole();
  }

  isChecker = ():boolean => this.userService.isChecker(this.userRole);

  get control() { return this.form.controls; }

  authorize(): void {
    this.clicked = true;
    this.alertService.clear();

    if (this.form.invalid) { return }
    this.progress = true;
    const eodDate = this.control['eodDate'].value;

    this.apiService.authCheck(this.form.value).subscribe({
      next: data => {
        this.progress = false
        this.checks = duration(data)
        this.alertService.success(`Entries for ${eodDate} was authorized successfully`);
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
        if(Object.keys(data).length === 0){
          this.alertService.warn(`No entry for ${this.control['eodDate'].value}`)
        }
        this.checks = duration(data)
      },
      error: err => {
        this.loading = false
        this.alertService.error("Something went wrong, please try again" + err)
        console.log(err)
      }
    });
  }

}
