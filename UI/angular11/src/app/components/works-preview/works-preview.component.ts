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
  filteredWorks: OrderRootReadDto[]

  active: boolean = true
  completed: boolean = false

  constructor(private router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.GetConfirmedWorksForUser().subscribe(res => {
      this.works = res
      this.onFilterChanged()
    })
  }

  getInfo(order: OrderRootReadDto) {
    this.sharedService.viewedWork = order
    this.router.navigate(["OrderProgress"])
  }

  onActiveChange() {
    this.active = !this.active
    this.onFilterChanged()
  }

  onCompletedChange() {
    this.completed = !this.completed
    this.onFilterChanged()
  }

  onFilterChanged() {
    this.filteredWorks = []
    this.works.forEach(w => {
      if (this.active == true && w.isCompleted == false) {
        this.filteredWorks.push(w)
      }
      else if (this.completed == true && w.isCompleted == true) {
        this.filteredWorks.push(w)
      }
    })
  }
}
