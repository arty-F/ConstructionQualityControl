import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  paymentForm: FormGroup
  paymentType: string
  cost: number = 0

  constructor(private router: Router, private fb: FormBuilder, private sharedService: SharedService) { }

  get payment() { return this.paymentForm.get('payment') }

  ngOnInit(): void {
    this.paymentForm = this.fb.group({
      payment: ['', Validators.required]
    })

    for (let order of this.sharedService.creatingOrder.subOrders) {
      if (order.prePaid != undefined) {
        this.cost += order.prePaid
      }
      if (order.postPaid != undefined) {
        this.cost += order.postPaid
      }
      for (let subOrder of order.subOrders) {
        if (subOrder.prePaid != undefined) {
          this.cost += subOrder.prePaid
        }
        if (subOrder.postPaid != undefined) {
          this.cost += subOrder.postPaid
        }
      }
    }

    this.sharedService.creatingOrder.postPaid = this.cost
  }

  onPaymentChange(e) {
    this.paymentType = e.target.value
  }

  onSubmit() {
    this.sharedService.AddOrder().subscribe(res => {
      this.router.navigate(['Orders'])
    })
  }
}
