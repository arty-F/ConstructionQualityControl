import { ChangeDetectionStrategy, HostListener } from '@angular/core';
import { Component } from '@angular/core'
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  appForm: FormGroup
  title = 'angular11'
  isAuthenticated: boolean
  userName: string

  constructor(private authService: AuthenticationService) {
    authService.getUserName()
      .subscribe(res => this.userName = res)
  }

  logout() {
    this.authService.logout()
  }
}
