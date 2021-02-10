import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-orders-preview',
  templateUrl: './orders-preview.component.html',
  styleUrls: ['./orders-preview.component.css']
})
export class OrdersPreviewComponent implements OnInit {

  orders: OrderRootReadDto[]
  filteredOrders: OrderRootReadDto[]

  active: boolean = true
  unstarted: boolean = true
  completed: boolean = false

  constructor(private router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.GetOrders().subscribe(res => {
      this.orders = res
      this.onFilterChanged()
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
        return "выберите исполнителя (в списке: " + order.workOffers.length.toString() + ")"
      }
      else {
        return "исполнитель не назначен"
      }
    }
  }

  chooseWorker(order: OrderRootReadDto) {
    this.sharedService.viewedWork = order
    this.router.navigate(["ChooseWorker"])
  }

  getInfo(order: OrderRootReadDto) {
    this.sharedService.viewedWork = order
    this.router.navigate(["OrderProgress"])
  }

  onActiveChange() {
    this.active = !this.active
    this.onFilterChanged()
  }

  onUnstartedChanged() {
    this.unstarted = !this.unstarted
    this.onFilterChanged()
  }

  onCompletedChange() {
    this.completed = !this.completed
    this.onFilterChanged()
  }

  onFilterChanged() {
    this.filteredOrders = []

    this.orders.forEach(o => {
      if (this.active == true && o.isCompleted == false && o.isStarted == true) {
        this.filteredOrders.push(o)
      }
      else if (this.unstarted == true && o.isStarted == false) {
        this.filteredOrders.push(o)
      }
      else if (this.completed == true && o.isCompleted == true) {
        this.filteredOrders.push(o)
      }
    })
  }

  deleteOrder(order: OrderRootReadDto) {
    this.sharedService.DeleteOrder(order).subscribe(res => {
      const index = this.orders.indexOf(order, 0)
      if (index > -1) {
        this.orders.splice(index, 1)
      }
      this.onFilterChanged()
    })
  }
}
