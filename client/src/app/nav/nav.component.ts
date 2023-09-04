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
    console.log('password');
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log('response', response);
        this.loggedIn = true;
      },
      error: (err) => console.log('err', err),
    });
    console.log('login method', this.model);
  }

  logout() {
    console.log('click logout method');
    this.loggedIn = false;
    console.log('logout method', this.model);
  }
}
