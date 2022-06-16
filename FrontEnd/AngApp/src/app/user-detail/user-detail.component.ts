import { Component, OnInit, Input } from '@angular/core';
import { User } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  user: User | undefined;

  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location) {

  }

  ngOnInit(): void {
    this.getUser();
  }

  getUser(): void {
    const Id = parseInt(this.route.snapshot.paramMap.get('Id')!, 10);
    this.userService.getUser(Id).subscribe(user => this.user = user);
  }

  goBack(): void {
    this.location.back();
  }
}
