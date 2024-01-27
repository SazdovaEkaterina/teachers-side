import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegisterModel } from '../../models/register-model';
import { AuthenticationService } from '../../service/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

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
    const payload = this.formGroup.getRawValue() as RegisterModel;
    this.authenticationService.register(payload).subscribe(result => {
      console.log(result);
    })
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      userName: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

}
