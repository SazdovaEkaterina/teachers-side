import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  public selectedTabIndex: number = 0;

  constructor(@Inject(Router) private router: Router) {}

  public logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiration');
    this.router.navigate(['/login']);
  }

  public onTabChanged(matTabChangeEvent: MatTabChangeEvent) {
    this.selectedTabIndex = matTabChangeEvent.index;
  }
}
