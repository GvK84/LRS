import { Injectable } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { Alert, AlertType } from "./alert";

// TODO explain the use of this
@Injectable()
export class AlertService {

  private _subject = new Subject<Alert>();
  private _idx = 0;

  constructor() {
  }

  getObservable(): Observable<Alert> {
    return this._subject.asObservable();
  }

  info(title: string, message: string, timeout = 3000) {
    this._subject.next(new Alert(this._idx++, AlertType.Info, title, message, timeout));
  }

  success(title: string, message: string, timeout = 3000) {
    this._subject.next(new Alert(this._idx++, AlertType.Success, title, message, timeout));
  }

  warning(title: string, message: string, timeout = 3000) {
    this._subject.next(new Alert(this._idx++, AlertType.Warning, title, message, timeout));
  }

  error(title: string, message: string, timeout = 4000) {
    this._subject.next(new Alert(this._idx++, AlertType.Error, title, message, timeout));
  }

}
