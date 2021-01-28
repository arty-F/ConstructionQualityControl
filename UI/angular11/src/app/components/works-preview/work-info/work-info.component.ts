import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkReadDto } from 'src/app/dtos/order/work-read-dto';
import { WorkOfferCreateDto } from 'src/app/dtos/work-offer/work-offer-create-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-work-info',
  templateUrl: './work-info.component.html',
  styleUrls: ['./work-info.component.css']
})
export class WorkInfoComponent implements OnInit {

  work: WorkReadDto
  summaryPrePaid: number = 0
  summaryPostPaid: number = 0
  message: string

  constructor(private sharedService: SharedService, private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    this.sharedService.GetViewedWorkInfo().subscribe(res => {
      this.work = res
      this.setSummary()
    })
  }

  private setSummary() {
    this.work.subOrders.forEach(e => {
      this.summaryPrePaid += e.prePaid
      this.summaryPostPaid += e.postPaid
      e.subOrders.forEach(se => {
        this.summaryPrePaid += se.prePaid
        this.summaryPostPaid += se.postPaid
      })
    })
  }

  onOffer() {
    let offer = new WorkOfferCreateDto()
    offer.worker = this.authService.user
    offer.message = this.message
    this.sharedService.AddOfferToViewedWork(offer).subscribe(r => {
      this.router.navigate(["Works/Find"]);
    })
  }
}
