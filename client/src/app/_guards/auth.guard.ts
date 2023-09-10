import { inject } from '@angular/core';
import { AccountsService } from './../_services/accounts.service';
import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountsService);
  const toastr = inject(ToastrService);

  // because currentUser is Observable --> need to use pipe()
  return accountService.currentUser$.pipe(
    map((user) => {
      if (user) return true;
      else {
        toastr.error('you shall not pass!');
        return false;
      }
    })
  );
};
