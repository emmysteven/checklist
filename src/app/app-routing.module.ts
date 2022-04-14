import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// const authModule = () => import('./auth').then(x => x.AuthModule);
// const homeModule = () => import('./home').then(x => x.HomeModule);


const routes: Routes = [
  // { path: '', loadChildren: homeModule },
  // { path: 'auth', loadChildren: authModule },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
