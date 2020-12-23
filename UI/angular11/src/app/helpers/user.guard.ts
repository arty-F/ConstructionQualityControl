import { Injectable } from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'

import { AuthenticationService } from 'src/app/services/authentication.service'

@Injectable({ providedIn: 'root' })
export class UserGuard implements CanActivate {

    private _role: string

    constructor(private router: Router, private authenticationService: AuthenticationService) {
        this.authenticationService.getUserRole()
            .subscribe(res =>
                this._role = res
            )
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let roles = route.data.roles as Array<string>
        if (this._role === roles[0]) {
            return true;
        }

        return false;
    }
}