import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderRootReadDto } from 'src/app/dtos/order/order-root-read-dto';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-works-preview',
  templateUrl: './works-preview.component.html',
  styleUrls: ['./works-preview.component.css']
})
export class WorksPreviewComponent implements OnInit {

  works: OrderRootReadDto[]

  constructor(private router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.GetConfirmedWorksForUser().subscribe(res => {
      this.works = res
    })
  }

  getInfo(order: OrderRootReadDto) {
    this.sharedService.viewedWork = order
    this.router.navigate(["OrderProgress"])
  }
}
