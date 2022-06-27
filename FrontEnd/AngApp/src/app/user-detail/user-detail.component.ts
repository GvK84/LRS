import { Component, OnInit, Input, AfterViewChecked, AfterContentInit, AfterViewInit, AfterContentChecked } from '@angular/core';
import { User, Title, Type } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';




@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit{

  user: User | undefined;
  titles: Title[] = [];
  types: Type[] = [];


  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location) {

  }

  ngOnInit(): void {
    this.getTitles();
    this.getTypes();
    this.getUser();

  }

  onSelectTitle(TitleId: number){
    if (this.user) {
      this.user.userTitleId=TitleId;}
  }

  onSelectType(TypeId: number){
    if (this.user) {
      this.user.userTypeId=TypeId;}
  }

  getUser(): void {
    const Id = parseInt(this.route.snapshot.paramMap.get('Id')!, 10);
    this.userService.getUser(Id).subscribe(user => this.user = user);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.user) {
      this.userService.updateUser(this.user)
        .subscribe(() => this.goBack());
    }
  }

  delete(): void {
    if (this.user) {
      this.userService.deleteUser(this.user)
        .subscribe(() => this.goBack());
    }
  }

  getTitles(): void {
    this.userService.getTitles().subscribe(titles => this.titles = titles);
  }

  getTypes(): void {
    this.userService.getTypes().subscribe(types => this.types = types);
  }
}
