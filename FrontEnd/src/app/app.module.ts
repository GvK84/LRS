import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { UsersComponent } from './users/users.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { ApiuserService } from './apiuser.service';
import { MapComponent } from './map/map.component';
import { UserAddComponent } from './user-add/user-add.component';
import { CommonModule } from '@angular/common';
import { ErrorInterceptor } from './error.interceptor';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';


@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    UsersComponent,
    HomeComponent,
    UserDetailComponent,
    MapComponent,
    UserAddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule, LoggerModule.forRoot({serverLoggingUrl: 'api/logs', level: NgxLoggerLevel.LOG, serverLogLevel: NgxLoggerLevel.WARN}),
  ],
  providers: [ApiuserService, {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
