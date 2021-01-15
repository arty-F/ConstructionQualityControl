import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CityService } from 'src/app/services/city.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-orders-new-root',
  templateUrl: './orders-new-root.component.html',
  styleUrls: ['./orders-new-root.component.css']
})
export class OrdersNewRootComponent implements OnInit {

  newOrderForm: FormGroup
  rootOrder: OrderCreateDto
  summary: string
  cities: string[]

  constructor(private router: Router, private fb: FormBuilder, public cityService: CityService, private sharedService: SharedService, private authService: AuthenticationService) {
    this.cityService = cityService
    this.rootOrder = new OrderCreateDto()
    this.rootOrder.isRoot = true
    this.rootOrder.user = authService.user
    this.rootOrder.subOrders = []
    sharedService.creatingOrder = this.rootOrder
  }

  get title() { return this.newOrderForm.get('title') }
  get city() { return this.newOrderForm.get('city') }

  ngOnInit() {
    this.newOrderForm = this.fb.group({
      title: ['', Validators.required],
      city: ['', Validators.required]
    })

    this.cityService.GetCityList().subscribe(res => {
      this.cities = res
      this.city.setValidators([Validators.required, CustomValidators.containInList(res)])
      this.city.setValue(this.authService.user.city.name + ', ' + this.authService.user.city.region.name)
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
    this.rootOrder.city = this.cityService.ConvertStrToCityDto(this.city.value)
    this.rootOrder.demands = this.title.value;
    this.router.navigate(['Payment'])
  }
}
