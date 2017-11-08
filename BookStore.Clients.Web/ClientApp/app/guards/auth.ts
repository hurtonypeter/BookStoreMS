import { Injectable } from '@angular/core';
import { Router, CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

import { SecurityService } from '../services/security.service';

import { Observable } from "rxjs/Observable";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private securityService: SecurityService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
        return true;
        //if (this.securityService.IsAuthorized) {
        //    return true;
        //}
        //else {
        //    this.router.navigate(['/login']);
        //    return false;
        //}
    }

}