import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UserCreateDto } from '../dtos/user/user-create-dto';
import { Observable } from 'rxjs';
import { SharedService } from '../shared.service';
import { CityReadDto } from '../dtos/city/city-read-dto';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup
  userDto: UserCreateDto = new UserCreateDto()
  cities: string[] = ['aaa', 'bbb', 'ccc', 'abb', 'acc']

  constructor(private fb: FormBuilder, private service: SharedService) { }

  get email() { return this.registerForm.get('email') }
  get lname() { return this.registerForm.get('lname') }
  get fname() { return this.registerForm.get('fname') }
  get patronymic() { return this.registerForm.get('patronymic') }
  get city() { return this.registerForm.get('city') }

  ngOnInit(): void {
    //this.service.GetCityList().subscribe(data => { this.cities = data })

    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      lname: ['', Validators.required],
      fname: ['', Validators.required],
      patronymic: ['', Validators.required],
    })
  }

  onSubmit(form): void {
    this.userDto.login = this.registerForm.get('email').value

    console.log(this.userDto.login)
  }
}