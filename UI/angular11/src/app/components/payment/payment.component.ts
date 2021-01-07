import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  paymentForm: FormGroup
  paymentType: string

  constructor(private fb: FormBuilder) { }

  get payment() { return this.paymentForm.get('payment') }

  ngOnInit(): void {
    this.paymentForm = this.fb.group({
      payment: ['', Validators.required]
    })
  }

  onPaymentChange(e) {
    this.paymentType = e.target.value
  }

  onSubmit() {
    console.log(this.paymentType)
  }

}
