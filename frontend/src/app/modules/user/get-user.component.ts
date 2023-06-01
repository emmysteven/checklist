import { Component } from '@angular/core';
import {AlertService, UserService} from "@app/core/services";
import { faUserEdit } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-get-user',
  templateUrl: './get-user.component.html',
  styles: []
})
export class GetUserComponent {
  protected readonly faUserEdit = faUserEdit;
  users: any;
  loading = false;
  submitted = false;

  constructor(
    private alertService: AlertService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: data => {
        this.users = data;
        console.log(data)
      },
      error: err => { console.log(err) }
    })

  }
}
