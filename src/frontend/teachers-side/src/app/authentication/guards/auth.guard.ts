import { Inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

import { UserService } from '../service/user.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    @Inject(Router) private router: Router,
    @Inject(UserService) private userService: UserService,){
  }

  canActivate(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(!this.userService.isLoggedIn()){
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
  
}
