import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  constructor(
    @Inject(Router) private router: Router,
    ) {
  }

  public logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiration');
    this.router.navigate(['/login']);
  }
}
