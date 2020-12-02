import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { StateService } from '../services/state.service';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  constructor(
    private stateService: StateService,
    private router: Router,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      map(event => event),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401 && this.router.url !== '/') {
          this.stateService.logout();
          this.router.navigate(['/'])
        }
        return throwError(error);
      })
    );
  }

}
