import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { HttpClient, } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { User, Title, Type } from './user';
import { NGXLogger } from 'ngx-logger';
import { AlertService } from '../alertfiles/alert.service';

@Injectable({
  providedIn: 'root'
})

export class ApiuserService {

  usersUrl = 'api/users/';
  titlesUrl = 'api/users/titles/';
  typesUrl = 'api/users/types/';

  constructor(private http: HttpClient, private logger: NGXLogger, private alertService: AlertService) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl).pipe(catchError(this.handleError<User[]>(`getUsers`, [])));
  }

  getActiveUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl + 'active').pipe(catchError(this.handleError<User[]>(`getActiveUsers`, [])));
  }

  getTitles(): Observable<Title[]> {
    return this.http.get<Title[]>(this.titlesUrl).pipe(catchError(this.handleError<Title[]>(`getTitles`, [])));
  }

  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(this.typesUrl).pipe(catchError(this.handleError<Type[]>(`getTypes`, [])));
  }

  getUser(id: Number): Observable<User> {
    return this.http.get<User>(this.usersUrl + id).pipe(catchError(this.handleError<User>(`getUser with id=${id}`)));
  }

  updateUser(user: User): Observable<any> {
    return this.http.put((this.usersUrl + user.id), user).pipe(catchError(this.handleError<any>(`updateUser with id=${user.id}`)));
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.usersUrl,user).pipe(catchError(this.handleError<User>(`addUser`)));
  }

  deleteUser(user: User): Observable<any> {
      user.isActive=false;
      return this.http.put((this.usersUrl + user.id), user).pipe(catchError(this.handleError<any>(`deleteUser with id=${user.id}`)));
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.logger.error(`${operation} failed: ${error.message}`);
      this.alertService.error("Error", `${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}
