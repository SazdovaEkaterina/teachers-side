import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegisterModel } from '../../models/register-model';
import { AuthenticationService } from '../../service/authentication.service';
import { Router } from '@angular/router';
import { tap, catchError, EMPTY, takeUntil, Subject } from 'rxjs';
import { IValidatorRule } from '../../models/validator-rule';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  public formGroup: FormGroup = this.formBuilder.group({});
  public errorMessage: string = '';

  public passwordValidatorRules: IValidatorRule[] = [
    {
      id: 0,
      text: 'Password must be between 8 and 20 characters long.',
      pattern: new RegExp('^[a-zA-Z0-9.*\\W.*]{8,20}$'),
    },
    {
      id: 1,
      text: 'Password must contain a lowercase letter.',
      pattern: new RegExp('.*[a-z]'),
    },
    {
      id: 2,
      text: 'Password must contain an uppercase letter.',
      pattern: new RegExp('.*[A-Z]'),
    },
    {
      id: 3,
      text: 'Password must contain a number.',
      pattern: new RegExp('.*\\d'),
    },
    {
      id: 4,
      text: 'Password must contain a special character.',
      pattern: new RegExp('.*\\W.*'),
    },
  ];

  public userNameValidatorRules: IValidatorRule[] = [
    {
      id: 0,
      text: 'User name must be between 8 and 20 characters long.',
      pattern: new RegExp('^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$'),
    },
  ];

  public emailValidatorRules: IValidatorRule[] = [
    {
      id: 0,
      text: 'Email must be a valid email address.',
      pattern: new RegExp('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
    },
  ];

  private userNameRegex: string =
    '^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$';
  private emailRegex: string = '^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$';
  private passwordRegex: string =
    '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,20}$';

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
    const payload = this.formGroup.getRawValue() as RegisterModel;
    this.authenticationService
      .register(payload)
      .pipe(
        tap(() => {
          this.navigateToLogin();
        }),
        catchError((error) => {
          if (error.status === 409) {
            this.errorMessage = error.error;
          } else {
            this.errorMessage = 'Something went wrong, please try again.';
          }
          return EMPTY;
        }),
        takeUntil(this.ngUnsubscribe)
      )
      .subscribe();
  }

  public navigateToLogin() {
    this.router.navigate(['/login']);
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  private buildForm() {
    this.formGroup = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      userName: [
        '',
        [Validators.required, Validators.pattern(this.userNameRegex)],
      ],
      email: ['', [Validators.required, Validators.pattern(this.emailRegex)]],
      password: [
        '',
        [Validators.required, Validators.pattern(this.passwordRegex)],
      ],
    });
  }
}
