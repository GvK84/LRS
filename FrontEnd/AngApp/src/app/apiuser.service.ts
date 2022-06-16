import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';

import { User } from './user';
import { Users } from './users';



@Injectable({
  providedIn: 'root'
})
export class ApiuserService {

  baseUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content Type': 'application/json',
    }),
  };

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/users').pipe(retry(1),catchError(this.errorHandl));
  }

  getUser(id: Number): Observable<User> {
    return this.http.get<User>(this.baseUrl + '/users/' + id).pipe(retry(1),catchError(this.errorHandl));
  }

  errorHandl(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage='';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }

}
