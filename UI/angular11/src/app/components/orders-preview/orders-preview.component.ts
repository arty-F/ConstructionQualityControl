import { Component, OnInit } from '@angular/core';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-orders-preview',
  templateUrl: './orders-preview.component.html',
  styleUrls: ['./orders-preview.component.css']
})
export class OrdersPreviewComponent implements OnInit {

  orders: OrderRootReadDto[]

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.GetOrders().subscribe(res => {
      this.orders = res
    })
  }

  getFormatedDate(dateStr: Date): string {
    return this.sharedService.GetFormatedDate(dateStr)
  }

  getOrderStatus(order: OrderRootReadDto): string {
    if (order.isCompleted) {
      return "завершен"
    }
    else if (order.isStarted) {
      return "активен"
    }
    else {
      return "еще не начат"
    }
  }

  getWorker(order: OrderRootReadDto): string {
    if (order.isStarted || order.isCompleted) {
      return order.workOffers[0].worker.companyName
    }
    else {
      if (order.workOffers.length > 0) {
        return "выберите одного из " + order.workOffers.length.toString() + " исполнителя"
      }
      else {
        return "иполнитель не назначен"
      }
    }
  }
}
