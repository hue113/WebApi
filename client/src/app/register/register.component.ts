import { ToastrService } from 'ngx-toastr';
import { AccountsService } from './../_services/accounts.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // to pass data to other component
  model: any = {};

  constructor(
    private accountService: AccountsService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => this.cancel(),
      error: (err) => {
        console.log('err', err);
        this.toastr.error(err.error);
      },
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
