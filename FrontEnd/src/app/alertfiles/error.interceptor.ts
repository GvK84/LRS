import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NGXLogger } from 'ngx-logger';
import { AlertService } from './alert.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private logger: NGXLogger, private alertService: AlertService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
          let errorMsg = '';
          if (error.error instanceof ErrorEvent) {
              this.logger.log('This is client side error');
              errorMsg = `Error: ${error.error.message}`;
              this.alertService.error('This is client side error', errorMsg);

          } else {
              this.logger.log('This is server side error');
              errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
              this.alertService.error('This is server side error', errorMsg);
          }
          this.logger.error(errorMsg);
          return throwError(()=> new Error(errorMsg));
      })
  )
  }
}
