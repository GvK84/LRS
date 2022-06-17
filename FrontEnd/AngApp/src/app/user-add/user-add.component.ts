import { Component, OnInit, Input } from '@angular/core';
import { User } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { NewUser } from '../newuser';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {

  nuser: User = NewUser;


  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location) {

  }

  ngOnInit(): void {

  }

  goBack(): void {
    this.location.back();
  }

  /* save(): void {
    if (this.user) {
      this.userService.updateUser(this.user)
        .subscribe(() => this.goBack());
    }
  } */
  addnew(user: User): void {
    if (!user.name || !user.surname || !user.userTitleId || !user.userTypeId) { return; }
    this.userService.addUser(user as User).subscribe(() => this.goBack());
  }
}
