import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ColyseusClientService } from '../services/colyseus-client.service';

@Injectable({
  providedIn: 'root'
})
export class ControllerGuard implements CanActivate {
  constructor(private readonly colyseusClientService: ColyseusClientService, private readonly router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.colyseusClientService.isConnected$()
      .pipe(tap(isConnected => {
        if (!isConnected) {
          this.router.navigate(['/join']);
        }
      }));
  }

}
