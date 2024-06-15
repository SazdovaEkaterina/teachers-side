import { Inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './service/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    @Inject(Router) private router: Router,
    @Inject(AuthenticationService) private authenticationService: AuthenticationService,){
  }

  canActivate(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(this.authenticationService.isLoggedIn()){
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
  
}
