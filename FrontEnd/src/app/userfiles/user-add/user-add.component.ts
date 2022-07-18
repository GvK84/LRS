import { Component, OnInit } from '@angular/core';
import { User, Title, Type, } from '../user';
import { Location } from '@angular/common';
import { ApiuserService } from '../apiuser.service';
import { AlertService } from 'src/app/alertfiles/alert.service';
import { FormControl, FormGroup, Validators} from '@angular/forms';
import { NGXLogger } from 'ngx-logger';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['../allusers.css']
})
export class UserAddComponent implements OnInit {
  userForm!: FormGroup;
  titles: Title[] = [];
  types: Type[] = [];
  submitted = false;
  constructor(private location: Location, private userService: ApiuserService, private alertService: AlertService, private logger: NGXLogger) { }

  ngOnInit(): void {
    this.getTitles();
    this.getTypes();
    this.formInit();
  }

  formInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      birthDate: new FormControl(''),
      emailAddress: new FormControl(''),
      userTitleId: new FormControl(0, Validators.min(1)),
      userTypeId: new FormControl(0, Validators.min(1)),
      isActive: new FormControl (true, Validators.required)
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

  clear(): void {
    this.userForm.reset();
    this.formInit();

  }

  submit(): void {
    this.submitted=true;
    if (!this.userForm.valid){
      this.logger.warn("Missing required fields!");
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

  isFieldInvalid(fieldname: string): boolean {
    if (this.submitted && (this.userForm.get(fieldname)?.errors?.['required'] || this.userForm.get(fieldname)?.errors?.['min']))
    {
      return true;
    }
    else return false;
  }

}
