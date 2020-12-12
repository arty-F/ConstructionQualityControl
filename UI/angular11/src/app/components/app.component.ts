import { HostListener } from '@angular/core';
import { Component } from '@angular/core'
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angular11';

  constructor(private authService: AuthenticationService) { }

  /*@HostListener('window:unload', [ '$event' ])
  unloadHandler(event) {
    // ...
  }*/

  @HostListener('window:beforeunload', [ '$event' ])
  beforeUnloadHandler(event) {
    this.authService.logout()
  }
}
