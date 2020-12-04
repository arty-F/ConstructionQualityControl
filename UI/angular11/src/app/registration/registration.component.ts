import { Component, OnInit } from '@angular/core'
import { FormGroup, Validators, FormBuilder } from '@angular/forms'
import { UserCreateDto } from '../dtos/user/user-create-dto'
import { Observable } from 'rxjs'
import { SharedService } from '../shared.service'
import { CityReadDto } from '../dtos/city/city-read-dto'
import { CustomValidators } from '../validation/custom-validators'

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup
  userDto: UserCreateDto = new UserCreateDto()
  cities: CityReadDto[]
  citiesReadable: string[]
  userType: string

  constructor(private fb: FormBuilder, private service: SharedService) { }

  get user() { return this.registerForm.get('user') }
  get email() { return this.registerForm.get('email') }
  get lname() { return this.registerForm.get('lname') }
  get fname() { return this.registerForm.get('fname') }
  get patronymic() { return this.registerForm.get('patronymic') }
  get city() { return this.registerForm.get('city') }
  get companyName() { return this.registerForm.get('companyName') }
  get companyDescription() {return this.registerForm.get('companyDescription')}
  get birthDate() {return this.registerForm.get('birthDate')}
  get password() {return this.registerForm.get('password')}
  get confirmedPassword() {return this.registerForm.get('confirmedPassword')}

  ngOnInit(): void {
    this.service.GetCityList().subscribe(data => {
      this.cities = data
      this.citiesReadable = this.cities.map(c => c.name + ', ' + c.region.name)
      this.registerForm.controls["city"].setValidators([Validators.required, CustomValidators.containInList(this.citiesReadable)])
    })

    this.registerForm = this.fb.group({
      user: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lname: ['', Validators.required],
      fname: ['', Validators.required],
      patronymic: ['', Validators.required],
      city: ['', Validators.required],
      companyName: [''],
      companyDescription: [''],
      birthDate: ['', [Validators.required, CustomValidators.dateIsOk()]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmedPassword: ['', [Validators.required, CustomValidators.passwordsMatch(this.password.value, this.confirmedPassword.value).bind(this)]]
    })
  }

  onUserChanged(e): void {
    this.userType = e.target.value
  }

  onSubmit(form): void {
    this.userDto.role = this.registerForm.get('user').value
    this.userDto.login = this.registerForm.get('email').value
    this.userDto.lastName = this.registerForm.get('lname').value
    this.userDto.firstName = this.registerForm.get('fname').value
    this.userDto.patronymic = this.registerForm.get('patronymic').value
    let currentCity = this.registerForm.get('city').value.split(", ")
    this.userDto.city = this.cities.filter(c => c.name == currentCity[0] && c.region.name == currentCity[1])[0]
    this.userDto.companyName = this.registerForm.get('patronymic').value
    this.userDto.companyDescription = this.registerForm.get('companyDescription').value

    console.log(this.userDto)
  }
}