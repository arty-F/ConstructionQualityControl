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
    
  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.user = this.subRootOrder.user
    subOrder.isRoot = false
    this.subRootOrder.subOrders.push(subOrder)
  }
}