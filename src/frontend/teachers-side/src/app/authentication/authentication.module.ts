import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ValidationRulesComponent } from './components/validation-rules/validation-rules.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ValidationRulesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class AuthenticationModule { }
