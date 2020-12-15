import { Injectable } from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'

import { AuthenticationService } from 'src/app/services/authentication.service'

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    private _token: string

    constructor(private router: Router, private authenticationService: AuthenticationService) {
        this.authenticationService.getToken()
            .subscribe(res =>
                this._token = res
            )
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this._token) {
            return true;
        }

        this.router.navigate(['Auth'])
        return false;
    }
}