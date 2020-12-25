import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-orders-new',
  templateUrl: './orders-new.component.html',
  styleUrls: ['./orders-new.component.css']
})
export class OrdersNewComponent implements OnInit {

  newOrderForm: FormGroup

  constructor(private fb: FormBuilder) { }

  get name() { return this.newOrderForm.get('name') }

  ngOnInit(): void {
    this.newOrderForm = this.fb.group({
      name: ['', Validators.required]
    })
  }

  onSubmit(form) {
    
  }
}
