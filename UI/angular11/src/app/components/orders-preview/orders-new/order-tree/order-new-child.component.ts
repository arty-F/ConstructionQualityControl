import { Component, Inject, Input } from '@angular/core'
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { OrdersNewRootComponent } from '../orders-new-root.component';

@Component({
  selector: 'app-order-new-child',
  templateUrl: './order-new-child.component.html',
  styleUrls: ['./order-new-child.component.css']
})
export class OrdersNewChildComponent {

  @Input()
  subRootOrder: OrderCreateDto

  constructor() { }

  ngOnInit() {
    
  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.user = this.subRootOrder.user
    subOrder.isRoot = false
    this.subRootOrder.subOrders.push(subOrder)
  }
}