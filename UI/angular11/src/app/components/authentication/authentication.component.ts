import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { AuthenticationService } from 'src/app/services/authentication.service'

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css'],
  providers: [AuthenticationService]
})
export class AuthenticationComponent implements OnInit {

  authenticateForm: FormGroup

  constructor(private fb: FormBuilder, private service: AuthenticationService) { }

  get email() { return this.authenticateForm.get('email') }
  get password() { return this.authenticateForm.get('password') }

  ngOnInit(): void {
    this.authenticateForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit(form): void {
    this.service.login(this.email.value, this.password.value).subscribe(resp => {
      //this.router.navigate(['profile'])
      console.log(resp)
    })
      

  }
}
