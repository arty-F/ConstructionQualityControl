import { Component, ComponentRef, Inject, Input } from '@angular/core'
import { OrderCreateDto } from 'src/app/dtos/order/order-create-dto';
import { SharedService } from 'src/app/services/shared.service';
import { OrdersNewRootComponent } from '../orders-new-root.component';

@Component({
  selector: 'app-order-new-child',
  templateUrl: './order-new-child.component.html',
  styleUrls: ['./order-new-child.component.css']
})
export class OrdersNewChildComponent {

  @Input()
  subRootOrder: OrderCreateDto

  constructor(private sharedService: SharedService) { }

  ngOnInit() {

  }

  onSubOrderAdded() {
    let subOrder = new OrderCreateDto()
    subOrder.user = this.subRootOrder.user
    subOrder.isRoot = false
    this.subRootOrder.subOrders.push(subOrder)
  }

  destroyRoot() {
    const index = this.sharedService.creatingOrder.subOrders.indexOf(this.subRootOrder, 0);
    if (index > -1) {
      this.sharedService.creatingOrder.subOrders.splice(index, 1);
    }
  }

  destroyChild(subOrd: OrderCreateDto) {
    const index = this.subRootOrder.subOrders.indexOf(subOrd, 0);
    if (index > -1) {
      this.subRootOrder.subOrders.splice(index, 1);
    }
  }
}