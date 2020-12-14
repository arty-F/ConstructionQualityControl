import { Injectable } from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'

import { AuthenticationService } from 'src/app/services/authentication.service'

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private authenticationService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const token = this.authenticationService.getToken()
        if (token) {
            return true;
        }

        this.router.navigate(['Auth'])
        return false;
    }
}