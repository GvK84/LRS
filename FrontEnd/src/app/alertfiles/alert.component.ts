import { Component } from '@angular/core';
import { AlertService } from "./alert.service";
import { Alert, AlertType } from "./alert";
import { Subscription } from "rxjs";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent {

  alerts: Alert[] = [];
  private _subscription: Subscription = new Subscription;

  constructor(private _alertSvc: AlertService) { }

private _addAlert(alert: Alert) {
    this.alerts.push(alert);

    if (alert.timeout !== 0) {
      setTimeout(() => this.close(alert), alert.timeout);

    }
  }

 ngOnInit() {
    this._subscription = this._alertSvc.getObservable().subscribe(alert => this._addAlert(alert));
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

  close(alert: Alert) {
    this.alerts = this.alerts.filter(al => al.id !== alert.id);
  }


className(alert: Alert): string {

    let style: string;

    switch (alert.type) {

      case AlertType.Success:
        style = 'success';
        break;

      case AlertType.Warning:
        style = 'warning';
        break;

      case AlertType.Error:
        style = 'error';
        break;

      default:
        style = 'info';
        break;
    }

    return style;
  }
}
