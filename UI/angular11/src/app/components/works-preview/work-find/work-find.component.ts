import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CityReadDto } from 'src/app/dtos/city/city-read-dto';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-work-find',
  templateUrl: './work-find.component.html',
  styleUrls: ['./work-find.component.css']
})
export class WorkFindComponent implements OnInit {

  worksForm: FormGroup
  works: OrderRootReadDto[]
  cities: CityReadDto[]
  citiesReadable: string[]

  constructor(private fb: FormBuilder, private sharedService: SharedService, private authService: AuthenticationService) { }

  get city() { return this.worksForm.get('city') }

  ngOnInit(): void {

    this.sharedService.GetCityList().subscribe(res => {
      this.cities = res
      this.citiesReadable = this.cities.map(c => c.name + ', ' + c.region.name)
      this.city.setValidators([Validators.required, CustomValidators.containInList(this.citiesReadable)])
      this.city.setValue(this.authService.user.city.name + ', ' + this.authService.user.city.region.name)
    })

    this.worksForm = this.fb.group({
      city: ['', Validators.required]
    })

    this.city.valueChanges.subscribe(val => {
      if (!this.city.invalid) {
        let currentCity = this.city.value.split(", ")
        let cityDto = this.cities.filter(c => c.name == currentCity[0] && c.region.name == currentCity[1])[0]
        this.sharedService.GetOrdersOfCity(cityDto).subscribe(res => {
          this.works = res
        })
      }
    })
  }

  onCityRefreshed() {
    let currentCity = this.city.value.split(", ")
    let cityDto = this.cities.filter(c => c.name == currentCity[0] && c.region.name == currentCity[1])[0]
    this.sharedService.GetOrdersOfCity(cityDto).subscribe(res => {
      this.works = res
    })
  }
}
