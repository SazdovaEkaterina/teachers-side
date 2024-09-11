import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EMPTY, Subject, catchError, takeUntil, tap } from 'rxjs';
import { AuthenticationService } from '../../service/authentication.service';
import { LoginModel } from '../../models/login-model';
import { ITokenResponse } from '../../models/token-response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  public title: string = 'Welcome back';
  public subtitle: string = 'Enter your credentials to access your account';

  public formGroup: FormGroup = this.formBuilder.group({});
  public errorMessage: string = '';

  private emailRegex: string = '^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$';

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(AuthenticationService)
    private readonly authenticationService: AuthenticationService,
    @Inject(Router) private router: Router
  ) {}

  public ngOnInit(): void {
    this.buildForm();
  }

  public submit() {
    const payload = this.formGroup.getRawValue() as LoginModel;
    this.authenticationService
      .login(payload)
      .pipe(
        tap((response: ITokenResponse) => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('expiration', response.expiration.toString());
          this.router.navigate(['/']);
        }),
        catchError((error) => {
          if (error.status === 401) {
            this.errorMessage = 'Incorrect password.';
          } else if (error.status === 404) {
            this.errorMessage = 'Incorrect email or password.';
          } else {
            this.errorMessage = 'Something went wrong, please try again.';
          }
          return EMPTY;
        }),
        takeUntil(this.ngUnsubscribe)
      )
      .subscribe();
  }

  public navigateToRegister() {
    this.router.navigate(['/register']);
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.pattern(this.emailRegex)]],
      password: ['', [Validators.required]],
    });
  }
}
