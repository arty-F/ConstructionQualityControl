import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { WorkOfferReadDto } from 'src/app/dtos/work-offer/work-offer-read-dto';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-choose-worker',
  templateUrl: './choose-worker.component.html',
  styleUrls: ['./choose-worker.component.css']
})
export class ChooseWorkerComponent implements OnInit {

  order: OrderRootReadDto

  constructor(private router: Router, private sharedService: SharedService) { }

  ngOnInit() {
    this.order = this.sharedService.viewedWork
  }

  confirmOffer(offer: WorkOfferReadDto) {
    this.sharedService.ConfirmOffer(offer).subscribe(res => {
      this.router.navigate(["Orders"])
    })
  }

  getDate(date: Date): string{
    return this.sharedService.GetFormatedDate(date)
  }
}
