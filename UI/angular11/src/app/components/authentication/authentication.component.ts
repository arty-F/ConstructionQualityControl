import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { SharedService } from 'src/app/shared.service'

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  authenticateForm: FormGroup

  constructor(private fb: FormBuilder, private service: SharedService) { }

  get email() { return this.authenticateForm.get('email') }
  get password() { return this.authenticateForm.get('password') }

  ngOnInit(): void {
    this.authenticateForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit(form): void {
    this.service.Authenticate(this.email.value, this.password.value).subscribe(t => console.log(t))
  }
}
