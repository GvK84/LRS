export class Alert {

  constructor(
    public id: number,
    public type: AlertType,
    public title: string,
    public message: string,
    public timeout: number,
  ) { }

}

export enum AlertType {
  Success,
  Info,
  Warning,
  Error
}
