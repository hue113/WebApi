import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../_services/accounts.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  // loggedIn = false;
  currentUser$: Observable<User | null> = of(null); // Observable of type User

  constructor(public accountService: AccountsService) {}

  ngOnInit(): void {
    // this.getCurrentUser();
    this.currentUser$ = this.accountService.currentUser$;
  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe({
  //     next: (user) => (this.loggedIn = !!true), // if user -> return true; if not user -> return false
  //     error: (error) => console.log(error),
  //   });
  // }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log('response', response);
        // this.loggedIn = true;
      },
      error: (err) => console.log('err', err),
    });
  }

  logout() {
    this.accountService.logout(); // remove item from localStorage
    // this.loggedIn = false;
  }
}
