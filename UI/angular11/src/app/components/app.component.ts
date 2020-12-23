import { ChangeDetectionStrategy, HostListener } from '@angular/core'
import { Component } from '@angular/core'
import { FormBuilder, FormGroup } from '@angular/forms'
import { Router } from '@angular/router'
import { AuthenticationService } from 'src/app/services/authentication.service'
import { userRole } from '../models/user-roles'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  appForm: FormGroup
  isAuthenticated: boolean
  userName: string
  userRole: string

  constructor(private router: Router, private authService: AuthenticationService) {
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
