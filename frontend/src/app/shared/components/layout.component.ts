import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  template: `
    <div class="col-md-6 offset-md-3 mt-5 mb-5">
      <router-outlet></router-outlet>
    </div>
  `,
  styles: [`
    .mt-5 {
      padding-top: 6rem;
    }
    .mb-5 {
      padding-bottom: 2rem;
    }
  `]
})
export class LayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
