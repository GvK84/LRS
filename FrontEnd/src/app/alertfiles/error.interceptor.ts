import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AlertService } from './alert.service';


@Injectable()

export class ErrorInterceptor implements HttpInterceptor {
  constructor(private alertService: AlertService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) =>
        {
          let errorMsg = '';
          errorMsg = `Message: ${error.message}`;
          return throwError(()=> new Error(errorMsg));
        }
      )
    )
  }
}
