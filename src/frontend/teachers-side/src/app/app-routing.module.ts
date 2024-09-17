import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './authentication/components/login/login.component';
import { RegisterComponent } from './authentication/components/register/register.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './platform/components/home/home.component';
import { AuthGuard } from './authentication/guards/auth.guard';
import { EventsTabComponent } from './platform/components/events-tab/events-tab.component';


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
  },
  {
    path: 'eventsTab',
    component: EventsTabComponent 
  },
  {
    path: 'home',
    component: HomeComponent
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
