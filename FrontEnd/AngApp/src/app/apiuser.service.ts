import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { HttpClient, } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { MessageService } from './message.service';

import { User } from './user';





@Injectable({
  providedIn: 'root'
})
export class ApiuserService {

  usersUrl = 'https://localhost:5001/api/users';

  constructor(private http: HttpClient, private messageService: MessageService) {}

  private log(message: string) {
    this.messageService.add(`UserService: ${message}`);
  }
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl).pipe(tap(_=> this.log(`fetched users`)), catchError(this.handleError<User[]>(`getUsers`, [])));
  }

  getUser(id: Number): Observable<User> {
    return this.http.get<User>(this.usersUrl + "/" +id).pipe(tap(_=> this.log(`fetched user w/ id=${id}`)),catchError(this.handleError<User>(`getUser id=${id}`)));
  }


  updateUser(user: User): Observable<any> {
    return this.http.put((this.usersUrl + "/" +user.id), user).pipe(tap(_=> this.log(`updated user w/ id=${user.id}`)), catchError(this.handleError<any>(`updateUser id=${user.id}`)));
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.usersUrl,user).pipe(
      tap((newUser: User) => this.log(`added user w/ id=${newUser.id}`)),
      catchError(this.handleError<User>(`addUser`))
    );
  }

  deleteUser(user: User): Observable<any> {
      user.isActive=false;
      return this.http.put((this.usersUrl + "/" +user.id), user).pipe
      (tap(_=> this.log(`deleted user w/ id=${user.id}`)), catchError(this.handleError<any>(`deleteUser id=${user.id}`)));
  }


  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}
