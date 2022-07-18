import { Component, OnInit, ViewChild } from '@angular/core';
import {User} from '../user';
import { ApiuserService } from '../apiuser.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['../allusers.css']
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  results: boolean = false;
  showInactive = false;

  @ViewChild('query') query: any;

  constructor(private userService: ApiuserService) {
  }

  ngOnInit(): void {
    this.getActiveUsers();
  }

  getActiveUsers(): void {
    this.userService.getActiveUsers().subscribe(users => this.users = users);
  }

  getAllUsers(): void {
    this.userService.getUsers().subscribe(users => this.users = users);
  }

  toggleUsers(checked: any): void {
    if (checked) {
      this.getAllUsers();
    }
    else {
      this.getActiveUsers();
    }
  }

  delete(user:User): void {
    if (confirm(`Delete user with id ${user.id}?`)){
      this.users = this.users.filter(u => u !==user)
      this.userService.deleteUser(user).subscribe();
    }
  }

  findUser(query:string): void {
    this.users = this.users.filter(u => (u.surname.toLowerCase() + " " + u.name.toLowerCase()).match(query.toLowerCase()))
    this.results = true;
  }

  clear(): void {
    this.results = false;
    this.query.nativeElement.value = '';
    this.ngOnInit();
  }



}
