import { AccountsService } from './_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'client';
  users: any;

  constructor(
    private http: HttpClient,
    private accountService: AccountsService
  ) {}

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log('err', error),
      complete: () => console.log('request has completed'),
    });
  }

  setCurrentUser() {
    // const user: User = JSON.parse(localStorage.getItem('user')); // type error
    // const user: User = JSON.parse(localStorage.getItem('user')!); // use ! to remove type check
    // best is do like this:
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
