import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { NotificationQueueService } from '../services/notification-queue.service';
import { StateService } from '../services/state.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
  constructor(
    private notificationQueueService: NotificationQueueService,
    private stateService: StateService,
  ) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    if (this.stateService.loggedIn.getValue()) {
      return of(true);
    } else {
      this.notificationQueueService.addNotification('You cannot route here because you have not authenticated. Please log in.', 'danger');
      return of(false);
    }
  }
}
