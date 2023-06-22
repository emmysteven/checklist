import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService, ApiService, UserService } from "@app/core/services";
import { faCheckDouble, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';

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
  userRole: string | null = '';

  faCheckDouble = faCheckDouble;
  faMagnifyingGlass = faMagnifyingGlass;

  loading = false;
  submitted = false;

  progress = false;
  clicked = false;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
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

    this.apiService.authSummary(this.form.value).subscribe({
      next: data => {
        this.progress = false
        var dataArray = []
        dataArray.push(data)
        this.summary = dataArray
        this.alertService.success(`Entries for ${eodDate} was authorized successfully`);
      },
      error: err => {
        this.progress = false
        this.alertService.error("Something went wrong, please try again")
        console.log(err)
      }
    });
  }

  search() {
    this.submitted = true;
    this.alertService.clear();

    const eodDate = this.form.value.eodDate;

    if (this.form.invalid) { return }

    this.loading = true;

    this.apiService.getSummary(eodDate).subscribe({
      next: data => {
        this.loading = false
        if(Object.keys(data).length === 0){
          this.alertService.warn(`No entry for ${eodDate}`)
        }
        this.summary = data
      },
      error: err => {
        this.loading = false
        this.alertService.error("Something went wrong, please try again")
        console.log(err)
      }
    });
  }
}
