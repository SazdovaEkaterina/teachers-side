import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './authentication/components/login/login.component';
import { RegisterComponent } from './authentication/components/register/register.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './platform/components/home/home.component';
import { AuthGuard } from './authentication/auth.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard], 
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  providers: [AuthGuard],
})
export class AppRoutingModule { }
