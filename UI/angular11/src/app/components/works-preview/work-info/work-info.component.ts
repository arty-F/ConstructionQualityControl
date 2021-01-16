import { Component, OnInit } from '@angular/core';
import { WorkReadDto } from 'src/app/dtos/order/work-read-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-work-info',
  templateUrl: './work-info.component.html',
  styleUrls: ['./work-info.component.css']
})
export class WorkInfoComponent implements OnInit {

  work: WorkReadDto

  constructor(private sharedService: SharedService, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.sharedService.GetViewedWorkInfo().subscribe(res => {
      this.work = res
    })
  }

}
