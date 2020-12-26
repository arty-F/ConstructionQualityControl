import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { personalData } from 'src/app/models/personal-data';

@Component({
  selector: 'app-orders-new',
  templateUrl: './orders-new.component.html',
  styleUrls: ['./orders-new.component.css']
})
export class OrdersNewComponent implements OnInit {

  newOrderForm: FormGroup
  rootOrder: OrderCreateDto

  constructor(private fb: FormBuilder) {
    this.rootOrder = new OrderCreateDto()
    this.rootOrder.isRoot = true
    this.rootOrder.user = localStorage.getItem(personalData.UserName)
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
    subOrder.user = this.rootOrder.user
    this.rootOrder.subOrders.push(subOrder)
  }

  lastSubOrder() {
    return this.rootOrder.subOrders[this.rootOrder.subOrders.length - 1]
  }

  onSubmit(form) {

  }
}
