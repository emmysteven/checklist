import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs';

import { Alert, AlertType } from "@app/core/models/alert";
import { AlertService } from '@app/core/services/alert.service';

@Component({
  selector: 'alert',
  templateUrl: 'alert.component.html',
  styleUrls: ['alert.component.css'],
})
export class AlertComponent implements OnInit, OnDestroy {
  @Input() id = 'default-alert';
  @Input() fade = true;

  alerts: Alert[] = [];
  alertSubscription: Subscription = new Subscription;
  routeSubscription: Subscription = new Subscription;

  constructor(private router: Router, private alertService: AlertService) { }

  ngOnInit() {
    // subscribe to new alert notifications
    this.alertSubscription = this.alertService.onAlert(this.id)
      .subscribe(alert => {
        // clear alerts when an empty alert is received
        if (!alert.message) {
          // filter out alerts without 'keepAfterRouteChange' flag
          this.alerts = this.alerts.filter((x: any) => x.keepAfterRouteChange);

          // remove 'keepAfterRouteChange' flag on the rest
          this.alerts.forEach((x: any) => delete x.keepAfterRouteChange);
          return;
        }

        // add alert to array
        this.alerts.push(alert);

        // auto close alert if required
        if (alert.autoClose) {
          setTimeout(() => this.removeAlert(alert), 3000);
        }
      });

    // this.alertSubscription = this.alertService.onAlert(this.id)
    //   .subscribe(alert => {
    //       if (!alert.message) {
    //         this.alerts = [];
    //         return;
    //       }
    //       this.alerts.push(alert);
    //
    //       if (alert.autoClose) {
    //         setTimeout(() => this.removeAlert(alert), 3000);
    //       }
    //     }
    //   );

    // clear alerts on location change
    this.routeSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.alertService.clear(this.id);
      }
    });
  }

  ngOnDestroy() {
    // unsubscribe to avoid memory leaks
    this.alertSubscription.unsubscribe();
    this.routeSubscription.unsubscribe();
  }

  removeAlert(alert: Alert) {
    // check if already removed to prevent error on auto close
    if (!this.alerts.includes(alert)) return;

    if (this.fade) {
      // fade out alert
      const alertIndex = this.alerts.findIndex(x => x === alert);
      if (alertIndex >= 0) {
        this.alerts[alertIndex].fade = true;
        // remove alert after faded out
        setTimeout(() => {
          this.alerts.splice(alertIndex, 1);
        }, 250);
      }
    } else {
      // remove alert
      this.alerts = this.alerts.filter(x => x !== alert);
    }
  }


  cssClass(alert: Alert) {
    if (!alert) return;

    const classes = ['alert', 'alert-dismissable'];

    const alertTypeClass = {
      [AlertType.Success]: 'alert alert-success',
      [AlertType.Error]: 'alert alert-danger',
      [AlertType.Info]: 'alert alert-info',
      [AlertType.Warning]: 'alert alert-warning'
    }

    classes.push(alertTypeClass[alert.alertType]);

    if (alert.fade) { classes.push('fade') }
    return classes.join(' ');
  }
}
