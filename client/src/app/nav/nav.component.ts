import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(public accountService: AccountsService) {}

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (user) => (this.loggedIn = !!true), // if user -> return true; if not user -> return false
      error: (error) => console.log(error),
    });
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log('response', response);
        this.loggedIn = true;
      },
      error: (err) => console.log('err', err),
    });
  }

  logout() {
    this.accountService.logout(); // remove item from localStorage
    this.loggedIn = false;
  }
}
