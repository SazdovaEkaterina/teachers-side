import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegisterModel } from '../../models/register-model';
import { AuthenticationService } from '../../service/authentication.service';
import { Router } from '@angular/router';
import { tap, catchError, EMPTY, takeUntil, Subject } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {

  public formGroup: FormGroup = this.formBuilder.group({});
  public errorMessage: string = "";

  private userNameRegex: string = "^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$";
  private emailRegex: string = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
  private passwordRegex: string = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$";

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(AuthenticationService) private readonly authenticationService: AuthenticationService,
    @Inject(Router) private router: Router,
    ) {
  }

  public ngOnInit(): void {
    this.buildForm();
  }

  public submit() {
    const payload = this.formGroup.getRawValue() as RegisterModel;
    this.authenticationService.register(payload)
      .pipe(
        tap(() => {
          this.router.navigate(['/login']);
        }),
        catchError((error) => {
          if(error.status === 409){
            this.errorMessage = error.error;
          }
          else if(error.status === 400){
            this.errorMessage = error.errors.toString();
          }
          else {
            this.errorMessage = "Something went wrong, please try again.";
          }
          return EMPTY;
        }),
        takeUntil(this.ngUnsubscribe)
      ).subscribe();
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      userName: ['', [Validators.required, Validators.pattern(this.userNameRegex)]],
      email: ['', [Validators.required, Validators.pattern(this.emailRegex)]],
      password: ['', [Validators.required, Validators.pattern(this.passwordRegex)]],
    });
  }

}
