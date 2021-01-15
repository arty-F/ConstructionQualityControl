import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  cities: string[]
  byCity: boolean = true
  byCost: boolean = false

  constructor(private fb: FormBuilder, public cityService: CityService, private sharedService: SharedService, private authService: AuthenticationService) { }

  get city() { return this.worksForm.get('city') }
  get cost() { return this.worksForm.get('cost') }

  ngOnInit(): void {
    this.worksForm = this.fb.group({
      city: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]]
    })

    this.city.valueChanges.subscribe(val => {
      if (this.byCity && !this.city.invalid) {
        this.refreshWorks()
      }
    })

    this.cost.valueChanges.subscribe(val => {
      if (this.byCost && !this.cost.invalid) {
        this.refreshWorks()
      }
    })

    this.cityService.GetCityList().subscribe(res => {
      this.cities = res
      this.city.setValidators([Validators.required, CustomValidators.containInList(res)])
      this.city.setValue(this.authService.user.city.name + ', ' + this.authService.user.city.region.name)
    })
  }

  onByCityChanged() {
    this.byCity = !this.byCity
    if (this.byCity && !this.city.invalid || !this.byCity) {
      this.refreshWorks()
    }
  }

  onByCostChanged() {
    this.byCost = !this.byCost
    if (this.byCost && !this.cost.invalid || !this.byCost) {
      this.refreshWorks()
    }
  }

  private refreshWorks() {
    if (this.byCity) {
      let cityDto = this.cityService.ConvertStrToCityDto(this.city.value)
      this.sharedService.GetWorksOfCity(cityDto).subscribe(res => {
        this.works = this.filterByCost(res, this.cost.value)
      })
    }
    else {
      this.sharedService.GetWorks().subscribe(res => {
        this.works = this.filterByCost(res, this.cost.value)
      })
    }
  }

  private filterByCost(orders: OrderRootReadDto[], cost: number): OrderRootReadDto[] {
    if (this.byCost) {
      return orders.filter(o => o.postPaid >= this.cost.value)
    }
    else {
      return orders
    }
  }
}
