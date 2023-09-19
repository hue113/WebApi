import { Component, Input, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  // encapsulation: ViewEncapsulation.Emulated,
})
export class MemberCardComponent {
  // @Input() member: Member = {} as Member; // to trick TS, but empty {} is not a Member --> avoid use this
  // Use this: member: Member | undefined
  @Input() member: Member | undefined; // to receive data from MemberList component

  constructor() {}
}
