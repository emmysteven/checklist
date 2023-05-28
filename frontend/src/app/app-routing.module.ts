import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// const userModule = () => import('./modules/user').then(x => x.UserModule);
// const homeModule = () => import('./modules/home').then(x => x.HomeModule);


const routes: Routes = [
  // { path: '', loadChildren: homeModule },
  // { path: 'user', loadChildren: userModule },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
