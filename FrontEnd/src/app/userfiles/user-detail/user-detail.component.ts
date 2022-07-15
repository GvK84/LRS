import { Component, OnInit } from '@angular/core';
import { User, Title, Type } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AlertService } from 'src/app/alertfiles/alert.service';



@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['../allusers.css']
})
export class UserDetailComponent implements OnInit{

  user!: User;
  titles: Title[] = [];
  types: Type[] = [];
  userForm!: FormGroup;

  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location,private formBuilder: FormBuilder, private alertService: AlertService) {

  }

  ngOnInit() {
    this.getTitles();
    this.getTypes();
    this.getUser();
  }

  getUser(): void {
    const Id = parseInt(this.route.snapshot.paramMap.get('Id')!, 10);
    this.userService.getUser(Id).subscribe(user => {
      this.user = user;
      this.userForm = this.formBuilder.group(this.user);});
  }


  goBack(): void {
    if (this.userForm.dirty){
      if (confirm("Leave?\nChanges will be lost")){
        this.location.back();
      }
      return;
    }
    this.location.back();
  }

  save(): void {
    if (!this.userForm.valid){
      this.alertService.warning("Invalid!","Fill in required fields!");
      return;
    }
    this.userService.updateUser(this.userForm.value as User).subscribe(() => this.location.back());
  }


  getTitles(): void {
    this.userService.getTitles().subscribe(titles => this.titles = titles);
  }

  getTypes(): void {
    this.userService.getTypes().subscribe(types => this.types = types);
  }


}
