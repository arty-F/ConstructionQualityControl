import { Component, OnInit } from '@angular/core'
import { FormGroup, Validators, FormBuilder } from '@angular/forms'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { SharedService } from 'src/app/services/shared.service'
import { CustomValidators } from 'src/app/helpers/custom-validators'
import { userRole } from 'src/app/models/user-roles'
import { Router } from '@angular/router'
import { CityService } from 'src/app/services/city.service'

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup
  userType: string
  cities: string[]

  constructor(private router: Router, private fb: FormBuilder, public cityService: CityService, private sharedService: SharedService) { }

  get user() { return this.registerForm.get('user') }
  get email() { return this.registerForm.get('email') }
  get lname() { return this.registerForm.get('lname') }
  get fname() { return this.registerForm.get('fname') }
  get patronymic() { return this.registerForm.get('patronymic') }
  get city() { return this.registerForm.get('city') }
  get companyName() { return this.registerForm.get('companyName') }
  get companyDescription() { return this.registerForm.get('companyDescription') }
  get birthDate() { return this.registerForm.get('birthDate') }
  get password() { return this.registerForm.get('password') }
  get confirmedPassword() { return this.registerForm.get('confirmedPassword') }

  ngOnInit() {
    this.registerForm = this.fb.group({
      user: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lname: ['', Validators.required],
      fname: ['', Validators.required],
      patronymic: ['', Validators.required],
      city: ['', Validators.required],
      companyName: [''],
      companyDescription: [''],
      birthDate: ['', [Validators.required, CustomValidators.dateIsMore17AndLess100()]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmedPassword: ['']
    })

    this.cityService.GetCityList().subscribe(res => {
      this.cities = res
      this.city.setValidators([Validators.required, CustomValidators.containInList(res)])
    })

    this.confirmedPassword.setValidators([Validators.required, CustomValidators.matchValueWith('password')])
  }

  onUserChanged(e) {
    this.userType = e.target.value
  }

  onSubmit(form) {
    let userDto = new UserCreateDto()
    userDto.role = this.user.value
    userDto.login = this.email.value
    userDto.lastName = this.lname.value
    userDto.firstName = this.fname.value
    userDto.patronymic = this.patronymic.value
    userDto.city = this.cityService.ConvertStrToCityDto(this.city.value)
    if (userDto.role === userRole.Builder) {
      userDto.companyName = this.companyName.value
      userDto.companyDescription = this.companyDescription.value
    }
    userDto.password = this.password.value

    this.sharedService.AddUser(userDto).subscribe(res => {
      this.router.navigate(['Auth'])
    })
  }
}