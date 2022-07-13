import { Component, OnInit, Input } from '@angular/core';
import { User, Title, Type, } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { AlertService } from 'src/app/alertfiles/alert.service';


@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {

  nuser: User = {
    id:0, name: "", surname: "",

    userTypeId: 1, userTitleId: 1,
    emailAddress: "", isActive: true, userTitleDesc: "Employee",
    userTypeDesc: "User"
  };
  titles: Title[] = [];
  types: Type[] = [];
  warningmsg: string = " ";


  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location, private alertService: AlertService) {

  }

  ngOnInit(): void {
    this.getTitles();
    this.getTypes();
  }

  goBack(): void {
    this.location.back();
  }


  addnew(user: User): void {
    if (!user.name) {
      this.warningmsg = "Fill in name!!!";
      this.alertService.add("Fill in name!!!");
      setTimeout(() => {
        this.warningmsg = " ";
      }, 4000)
      return; }
    this.userService.addUser(user as User).subscribe(() => this.goBack());
  }


  getTitles(): void {
    this.userService.getTitles().subscribe(titles => this.titles = titles);
  }

  getTypes(): void {
    this.userService.getTypes().subscribe(types => this.types = types);
  }

  onSelectTitle(TitleId: number){
    if (this.nuser) {
      this.nuser.userTitleId=TitleId;}
  }

  onSelectType(TypeId: number){
    if (this.nuser) {
      this.nuser.userTypeId=TypeId;}
  }
}
