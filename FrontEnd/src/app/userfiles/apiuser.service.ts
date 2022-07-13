import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { HttpClient, } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { User, Title, Type } from './user';
import { NGXLogger } from 'ngx-logger';





@Injectable({
  providedIn: 'root'
})
export class ApiuserService {

 /*  usersUrl = 'https://localhost:5001/api/users';
  titlesUrl = 'https://localhost:5001/api/titles';
  typesUrl = 'https://localhost:5001/api/types'; */
  usersUrl = 'api/users/';
  titlesUrl = 'api/titles/';
  typesUrl = 'api/types/';

  constructor(private http: HttpClient, private logger: NGXLogger) {}

    getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl).pipe(tap(_=> this.logger.log(`fetched users`)), catchError(this.handleError<User[]>(`getUsers`, [])));
  }

  getTitles(): Observable<Title[]> {
    return this.http.get<Title[]>(this.titlesUrl).pipe(tap(_ => this.logger.log(`fetched titles`)), catchError(this.handleError<Title[]>(`getTitles`, [])));
  }

  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(this.typesUrl).pipe(tap(_ => this.logger.log(`fetched types`)), catchError(this.handleError<Type[]>(`getTypes`, [])));
  }

  getUser(id: Number): Observable<User> {
    return this.http.get<User>(this.usersUrl + id).pipe(tap(_ => this.logger.log(`fetched user w/ id=${id}`)),catchError(this.handleError<User>(`getUser id=${id}`)));
  }


  updateUser(user: User): Observable<any> {
    return this.http.put((this.usersUrl + user.id), user).pipe(tap(_ => this.logger.log(`updated user w/ id=${user.id}`)), catchError(this.handleError<any>(`updateUser id=${user.id}`)));
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.usersUrl,user).pipe(
      tap(_ => this.logger.log(`added user`)),
      catchError(this.handleError<User>(`addUser`))
    );
  }

  deleteUser(user: User): Observable<any> {
      user.isActive=false;
      return this.http.put((this.usersUrl + user.id), user).pipe
        (tap(_ => this.logger.log(`deleted user w/ id=${user.id}`)), catchError(this.handleError<any>(`deleteUser id=${user.id}`)));
  }


  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      this.logger.error(error);
      this.logger.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}
