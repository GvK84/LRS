import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { UsersComponent } from './users/users.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { ApiuserService } from './apiuser.service';
import { MapComponent } from './map/map.component';
import { MessagesComponent } from './messages/messages.component';
import { UserAddComponent } from './user-add/user-add.component';


@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    UsersComponent,
    HomeComponent,
    UserDetailComponent,
    MapComponent,
    MessagesComponent,
    UserAddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ApiuserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
