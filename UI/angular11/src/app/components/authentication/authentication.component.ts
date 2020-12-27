import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router'
import { Observable, throwError } from 'rxjs'
import { catchError, first } from 'rxjs/operators'
import { personalData } from 'src/app/models/personal-data'
import { AuthenticationService } from 'src/app/services/authentication.service'

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  authenticateForm: FormGroup
  authError: boolean

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthenticationService) { }

  get email() { return this.authenticateForm.get('email') }
  get password() { return this.authenticateForm.get('password') }

  ngOnInit() {
    this.authenticateForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit(form) {
    this.authService.login(this.email.value, this.password.value)
      .subscribe(
        res => {
          this.authError = false
          this.router.navigate(['Orders'])
        },
        err => {
          this.authError = true
        }
      )
  }
}