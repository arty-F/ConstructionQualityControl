import { Component, Inject, Input } from '@angular/core'
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { OrdersNewComponent } from '../orders-new.component';

@Component({
  selector: 'app-order-tree',
  templateUrl: './order-tree.component.html',
  styleUrls: ['./order-tree.component.css']
})
export class OrderTreeComponent {

  @Input()
  subRootOrder: OrderCreateDto

  constructor() { }

  ngOnInit() {
    console.log(this.subRootOrder.user)
  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.user = this.subRootOrder.user
    this.subRootOrder.subOrders.push(subOrder)
    console.log(this.subRootOrder.subOrders.length)
  }
}