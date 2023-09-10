import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../_services/accounts.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  currentUser$: Observable<User | null> = of(null); // Observable of type User

  constructor(
    public accountService: AccountsService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: (err) => this.toastr.error(err.error),
    });
  }

  logout() {
    this.accountService.logout(); // remove item from localStorage
    this.router.navigateByUrl('/');
  }
}
