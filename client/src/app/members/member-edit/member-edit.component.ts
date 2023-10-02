import { MembersService } from './../../_services/members.service';
import { AccountsService } from './../../_services/accounts.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  // @ViewChild: to access a specific child component or element in the template
  // Eg: editForm in this case, so you can reset that form after submitting
  @ViewChild('editForm') editForm: NgForm | undefined;

  // @HostListener: to detect when user click go back/go forward in browser --> to notify them of unsaved changes
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

  member: Member | undefined;
  user: User | null = null;

  constructor(
    private accountsService: AccountsService,
    private memberService: MembersService,
    private toastr: ToastrService
  ) {
    this.accountsService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => (this.user = user),
    });
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if (!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: (member) => (this.member = member),
    });
  }

  updateMember() {
    this.toastr.success('Profile updated successfully');
    this.editForm?.reset(this.member); // to reset the notification on top (remove it) & disable submit button
  }
}
