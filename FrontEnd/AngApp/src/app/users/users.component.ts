import { Component, OnInit } from '@angular/core';
import {User} from '../user';
import { ApiuserService } from '../apiuser.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: ApiuserService) {
  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.userService.getUsers().subscribe(users => this.users = users);
      /* = users.filter(u => u.isActive==true)); */
  }

  delete(user:User): void {
    this.users = this.users.filter(u => u !==user)
    this.userService.deleteUser(user).subscribe();
  }

  findUser(query:string): void {
    this.users=this.users.filter(u => (u.surname+" "+u.name).match(query))
  }

}
