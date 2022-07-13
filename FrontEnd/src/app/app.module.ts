import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './navfiles/menu/menu.component';
import { UsersComponent } from './userfiles/users/users.component';
import { HomeComponent } from './navfiles/home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './userfiles/user-detail/user-detail.component';
import { ApiuserService } from './userfiles/apiuser.service';
import { MapComponent } from './mapfiles/map.component';
import { UserAddComponent } from './userfiles/user-add/user-add.component';
import { CommonModule } from '@angular/common';
import { ErrorInterceptor } from './alertfiles/error.interceptor';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';
import { AlertComponent } from './alertfiles/alert.component';


@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    UsersComponent,
    HomeComponent,
    UserDetailComponent,
    MapComponent,
    UserAddComponent,
    AlertComponent
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
