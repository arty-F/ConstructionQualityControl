import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CityReadDto } from 'src/app/dtos/city/city-read-dto';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CityService } from 'src/app/services/city.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-work-find',
  templateUrl: './work-find.component.html',
  styleUrls: ['./work-find.component.css']
})
export class WorkFindComponent implements OnInit {

  worksForm: FormGroup
  works: OrderRootReadDto[]

  constructor(private fb: FormBuilder, public cityService: CityService, private sharedService: SharedService, private authService: AuthenticationService) { }

  get city() { return this.worksForm.get('city') }

  ngOnInit(): void {
    this.worksForm = this.fb.group({
      city: [this.authService.user.city.name + ', ' + this.authService.user.city.region.name, [Validators.required, CustomValidators.containInList(this.cityService.citiesReadable)]]
    })

    this.city.valueChanges.subscribe(val => {
      if (!this.city.invalid) {
        let cityDto = this.cityService.ConvertStrToCityDto(this.city.value)
        this.sharedService.GetOrdersOfCity(cityDto).subscribe(res => {
          this.works = res
        })
      }
    })
  }
}
