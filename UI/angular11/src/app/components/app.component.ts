import { Component } from '@angular/core'
import { Router } from '@angular/router'
import { AuthenticationService } from 'src/app/services/authentication.service'
import { userRole } from '../models/user-roles'
import { CityService } from '../services/city.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isAuthenticated: boolean
  userName: string
  userRole: string

  constructor(private router: Router, private authService: AuthenticationService, private cityService: CityService) {
    authService.getUserName()
      .subscribe(res =>
        this.userName = res
      )
    authService.getUserRole()
      .subscribe(res =>
        this.userRole = res
      )
  }

  logout() {
    this.authService.logout()
    this.router.navigate(['Auth'])
  }

  IsUserCustomer(): boolean {
    return this.userRole === userRole.Customer
  }

  IsUserBuilder(): boolean {
    return this.userRole === userRole.Builder
  }
}
