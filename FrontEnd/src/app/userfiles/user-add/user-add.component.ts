import { Component, OnInit } from '@angular/core';
import { User, Title, Type, } from '../user';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { AlertService } from 'src/app/alertfiles/alert.service';
import { FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['../allusers.css']
})
export class UserAddComponent implements OnInit {
  userForm = new FormGroup({
    name: new FormControl('', Validators.required),
    surname: new FormControl(''),
    birthDate: new FormControl(''),
    emailAddress: new FormControl(''),
    userTitleId: new FormControl(0, Validators.required),
    userTypeId: new FormControl(0, Validators.required),
    isActive: new FormControl (true, Validators.required)
  });
  titles: Title[] = [];
  types: Type[] = [];

  constructor(private location: Location, private userService: ApiuserService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.getTitles();
    this.getTypes();
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

  clear(): void {
    this.userForm.reset()
  }

  submit(): void {
    if (!this.userForm.valid || (this.userForm.value.userTypeId==0) || (this.userForm.value.userTitleId==0)){
      this.alertService.warning("Invalid!","Fill in required fields!");
      return;
    }
    this.userService.addUser(this.userForm.value as User).subscribe(() => this.clear());
  }

  getTitles(): void {
    this.userService.getTitles().subscribe(titles => this.titles = titles);
  }

  getTypes(): void {
    this.userService.getTypes().subscribe(types => this.types = types);
  }
}
