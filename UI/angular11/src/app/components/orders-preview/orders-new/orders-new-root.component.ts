import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, of, Subject } from 'rxjs';
import { CityReadDto } from 'src/app/dtos/city/city-read-dto';
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { personalData } from 'src/app/models/personal-data';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-orders-new-root',
  templateUrl: './orders-new-root.component.html',
  styleUrls: ['./orders-new-root.component.css']
})
export class OrdersNewRootComponent implements OnInit {

  newOrderForm: FormGroup
  rootOrder: OrderCreateDto
  cities: CityReadDto[]
  citiesReadable: string[]
  summary: string

  constructor(private router: Router, private fb: FormBuilder, private sharedService: SharedService, private authService: AuthenticationService) {
    this.rootOrder = new OrderCreateDto()
    this.rootOrder.isRoot = true
    this.rootOrder.user = authService.user
    this.rootOrder.subOrders = []
    sharedService.creatingOrder = this.rootOrder
  }

  get title() { return this.newOrderForm.get('title') }
  get city() { return this.newOrderForm.get('city') }

  ngOnInit() {
    this.sharedService.GetCityList().subscribe(data => {
      this.cities = data
      this.citiesReadable = this.cities.map(c => c.name + ', ' + c.region.name)
      this.city.setValidators([Validators.required, CustomValidators.containInList(this.citiesReadable)])
      this.city.setValue(this.authService.user.city.name + ', ' + this.authService.user.city.region.name)
    })

    this.newOrderForm = this.fb.group({
      title: ['', Validators.required],
      city: ['', Validators.required]
    })
  }

  ngOnChanges() {
      this.summary = this.rootOrder.demands
  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.subOrders = []
    subOrder.user = this.rootOrder.user
    subOrder.isRoot = false
    this.rootOrder.subOrders.push(subOrder)
  }

  onSubmit(form) {
    let currentCity = this.city.value.split(", ")
    this.rootOrder.city = this.cities.filter(c => c.name == currentCity[0] && c.region.name == currentCity[1])[0]
    this.rootOrder.demands = this.title.value;
    this.router.navigate(['Payment'])
  }
}
