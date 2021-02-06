import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommentCreateDto } from 'src/app/dtos/comment/comment-create-dto';
import { OrderReadDto } from 'src/app/dtos/order/order-read-dto';
import { ReportCreateDto } from 'src/app/dtos/report/report-create-dto';
import { UserReadDto } from 'src/app/dtos/user/user-read-dto';
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
  comment: CommentCreateDto = new CommentCreateDto()
  urls = []

  constructor(private router: Router, private sharedService: SharedService, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.sharedService.GetOrder().subscribe(res => {
      this.order = res
    })
    this.comment.user = this.authService.user
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
    if (this.isUserBuilder()) {
      return 'заказчик'
    }
    else {
      return 'исполнитель работ'
    }
  }

  getUserName(): string {
    if (this.order) {
      if (this.isUserBuilder()) {
        return this.order.user.lastName + ' ' + this.order.user.firstName + ' ' + this.order.user.patronymic
      }
      else {
        return this.sharedService.viewedWork.workOffers[0].worker.companyName
      }
    }
    else {
      return ''
    }
  }

  getCommentUserName(user: UserReadDto): string {
    return user.companyName ? user.companyName : user.lastName + ' ' + user.firstName + ' ' + user.patronymic
  }

  getCommentDate(date: Date): string {
    let d = new Date(date)
    let hour = d.getHours().toString()
    if (hour.length == 1) {
      hour = '0' + hour
    }
    let minute = d.getMinutes().toString()
    if (minute.length == 1) {
      minute = '0' + minute
    }
    return this.sharedService.GetFormatedDate(date) + ' ' + hour + ':' + minute
  }

  isUserCustomer(): boolean {
    return this.authService.user.role == userRole.Customer
  }

  isUserBuilder(): boolean {
    return this.authService.user.role == userRole.Builder
  }

  addComment(order: OrderReadDto) {
    this.sharedService.AddComment(this.comment, order.id).subscribe(res => {
      this.comment.text = ''
      this.getLastStartedOrder().comments.push(res)
    })
  }

  confirmSubOrder() {

  }

  getLastStartedOrder(): OrderReadDto {
    let result: OrderReadDto

    this.order.subOrders.forEach(o => {
      if (o.isStarted) {
        result = o
      }
      o.subOrders.forEach(so => {
        if (so.isStarted) {
          result = o
        }
      })
    })

    return result
  }

  selectFiles(event) {
    if (event.target.files) {
      for (var i = 0; i < event.target.files.length; i++) {
        var reader = new FileReader()
        reader.readAsDataURL(event.target.files[i])
        reader.onload = (event: any) => {
          this.urls.push(event.target.result)
        }
      }
    }
  }

  clearFiles() {
    this.urls = []
  }

  addReports(order: OrderReadDto) {
    let reports: ReportCreateDto[] = []
    this.urls.forEach(u => {
      let str = new String(u)
      let reportDto = new ReportCreateDto()
      reportDto.user = this.authService.user
      reportDto.order = this.order
      reportDto.data = str.toString()
      reports.push(reportDto)
    })

    this.sharedService.AddReports(reports, order.id).subscribe(res => {
      res.forEach(r => {
        this.getLastStartedOrder().reports.push(r)
      })
    })
  }
}
