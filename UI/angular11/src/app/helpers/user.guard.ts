import { Injectable } from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'

import { AuthenticationService } from 'src/app/services/authentication.service'
import { personalData } from '../models/personal-data'

@Injectable({ providedIn: 'root' })
export class UserGuard implements CanActivate {

    constructor(private router: Router, private authenticationService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let roles = route.data.roles as Array<string>
        if (localStorage.getItem(personalData.UserRole) === roles[0]) {
            return true
        }

        return false
    }
}