import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Observable, throwError } from 'rxjs'
import { catchError, first } from 'rxjs/operators'
import { jwtClaim } from 'src/app/models/jwt-claims'
import { AuthenticationService } from 'src/app/services/authentication.service'

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  authenticateForm: FormGroup

  constructor(private fb: FormBuilder, private authService: AuthenticationService) { }

  get email() { return this.authenticateForm.get('email') }
  get password() { return this.authenticateForm.get('password') }

  ngOnInit(): void {
    this.authenticateForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit(form): void {
    this.authService.login(this.email.value, this.password.value)
      .subscribe(
        res => {
          console.log(localStorage.getItem(jwtClaim.AccessToken))
        },
        err => {
          console.log('err')
        }
      )
  }
}
