import { Component, OnInit } from '@angular/core';
import { User, Title, Type } from '../user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/alertfiles/alert.service';
import { NGXLogger } from 'ngx-logger';



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
  submitted = false;

  constructor(private route: ActivatedRoute, private userService: ApiuserService, private location: Location, private logger: NGXLogger, private alertService: AlertService) {

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
        if (this.user){
          this.formInit(this.user);
        }
      });
  }

  formInit(user: User): void {
    this.userForm = new FormGroup({
      name: new FormControl(user.name, Validators.required),
      surname: new FormControl(user.surname, Validators.required),
      birthDate: new FormControl(user.birthDate),
      emailAddress: new FormControl(user.emailAddress),
      userTitleId: new FormControl(user.userTitleId, Validators.min(1)),
      userTypeId: new FormControl(user.userTypeId, Validators.min(1)),
      isActive: new FormControl (user.isActive, Validators.required)
    });
    this.submitted=false;
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
    this.submitted=true;
    if (!this.userForm.valid){
      this.logger.warn("Missing required fields!");
      this.alertService.warning("Invalid!","Fill in required fields!");
      return;
    }
    let nuser = this.userForm.value as User;
    nuser.id=this.user.id;
    this.userService.updateUser(nuser).subscribe(() => this.location.back());
  }


  getTitles(): void {
    this.userService.getTitles().subscribe(titles => this.titles = titles);
  }

  getTypes(): void {
    this.userService.getTypes().subscribe(types => this.types = types);
  }

  isFieldInvalid(fieldname: string): boolean {
    if (this.submitted && (this.userForm.get(fieldname)?.errors?.['required'] || this.userForm.get(fieldname)?.errors?.['min']))
    {
      return true;
    }
    else return false;
  }

  delete(): void {
    if (confirm(`Delete user with id ${this.user.id}?`)){

    this.userService.deleteUser(this.user).subscribe(() => this.location.back());
  }
}

}
