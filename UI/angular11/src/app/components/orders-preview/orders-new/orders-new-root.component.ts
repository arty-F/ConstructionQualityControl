import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
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

  constructor(private router: Router, private fb: FormBuilder, private sharedService: SharedService, private authService: AuthenticationService) {
    this.rootOrder = new OrderCreateDto()
    this.rootOrder.isRoot = true
    this.rootOrder.user = authService.user
    this.rootOrder.subOrders = []
  }

  get title() { return this.newOrderForm.get('title') }

  ngOnInit() {
    this.newOrderForm = this.fb.group({
      title: ['', Validators.required]
    })
  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.subOrders = []
    subOrder.user = this.rootOrder.user
    subOrder.isRoot = false
    this.rootOrder.subOrders.push(subOrder)
  }

  onSubmit(form) {
    this.rootOrder.demands = this.title.value;
    this.sharedService.AddOrder(this.rootOrder).subscribe(res =>
      this.router.navigate(['Orders'])
    )
  }
}
