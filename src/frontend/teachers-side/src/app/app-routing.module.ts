import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './authentication/components/login/login.component';
import { RegisterComponent } from './authentication/components/register/register.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './platform/components/home/home.component';
import { AuthGuard } from './authentication/guards/auth.guard';
import { EventCardComponent } from './platform/components/event-card/event-card.component';
import { EventsTabComponent } from './platform/components/events-tab/events-tab.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
    component: RegisterComponent
  },
  {
    path: 'events',
    component: EventsTabComponent
  }
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard],
})
export class AppRoutingModule {}
