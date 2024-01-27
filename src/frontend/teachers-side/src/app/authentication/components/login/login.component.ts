import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../service/authentication.service';
import { LoginModel } from '../../models/login-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public formGroup: FormGroup = this.formBuilder.group({});

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(AuthenticationService) private readonly authenticationService: AuthenticationService,
    ) {
  }

  ngOnInit(): void {
    this.buildForm();
  }

  public submit() {
    const payload = this.formGroup.getRawValue() as LoginModel;
    this.authenticationService.login(payload).subscribe(result => {
      console.log(result);
    })
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

}
