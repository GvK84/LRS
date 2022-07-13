import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './userfiles/users/users.component';
import { HomeComponent } from './navfiles/home/home.component';
import { UserDetailComponent } from './userfiles/user-detail/user-detail.component';
import { MapComponent } from './mapfiles/map.component';
import { UserAddComponent } from './userfiles/user-add/user-add.component';


const routes: Routes = [

  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'users', component: UsersComponent },
  { path: 'users/:Id', component: UserDetailComponent },
  { path: 'addnew', component: UserAddComponent },
  { path: 'map', component: MapComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
