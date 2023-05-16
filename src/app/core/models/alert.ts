export class Alert {
  id: string = '';
  type: AlertType = AlertType.INFO;
  message: string = '';
  autoClose: boolean = false;
  keepAfterRouteChange: boolean = false;
  fade: boolean = false;

  constructor(init?:Partial<Alert>) {
    Object.assign(this, init);
  }
}

export enum AlertType {
  SUCCESS = "success",
  ERROR = "error",
  INFO = "info",
  WARNING = "warning",
}
