import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { personalData } from 'src/app/models/personal-data';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-orders-new',
  templateUrl: './orders-new.component.html',
  styleUrls: ['./orders-new.component.css']
})
export class OrdersNewComponent implements OnInit {

  newOrderForm: FormGroup
  rootOrder: OrderCreateDto

  constructor(private fb: FormBuilder, private service: SharedService) {
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
    subOrder.subOrders = []
    subOrder.user = this.rootOrder.user
    subOrder.isRoot = false
    this.rootOrder.subOrders.push(subOrder)
  }

  onSubmit(form) {
    this.rootOrder.demands = this.title.value;
    this.service.AddOrder(this.rootOrder).subscribe(res =>
      console.log('ok')
    )
  }
}
