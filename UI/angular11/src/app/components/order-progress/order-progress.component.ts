import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { elementAt } from 'rxjs/operators';
import { OrderReadDto } from 'src/app/dtos/order/order-read-dto';
import { userRole } from 'src/app/models/user-roles';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-order-progress',
  templateUrl: './order-progress.component.html',
  styleUrls: ['./order-progress.component.css']
})
export class OrderProgressComponent implements OnInit {

  order: OrderReadDto

  constructor(private router: Router, private sharedService: SharedService, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.sharedService.GetOrder().subscribe(res => {
      this.order = res
    })
  }

  getHeader(): string {
    if (this.order) {
      return this.order.demands
    }
    else {
      return ''
    }
  }

  getCreationDate(): string {
    if (this.order) {
      return this.sharedService.GetFormatedDate(this.order.creationDate)
    }
    else {
      return ''
    }
  }

  getStartedDate(): string {
    if (this.order) {
      return this.sharedService.GetFormatedDate(this.sharedService.viewedWork.workOffers[0].date)
    }
    else {
      return ''
    }
  }

  getUserType(): string {
    if (this.authService.user.role == userRole.Builder) {
      return 'заказчик'
    }
    else {
      return 'исполнитель работ'
    }
  }

  getUserName(): string {
    if (this.authService.user.role == userRole.Builder) {
      return this.order.user.lastName + ' ' + this.order.user.firstName + ' ' + this.order.user.patronymic
    }
    else {
      return this.sharedService.viewedWork.workOffers[0].worker.companyName
    }
  }

}
