import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-order-progress',
  templateUrl: './order-progress.component.html',
  styleUrls: ['./order-progress.component.css']
})
export class OrderProgressComponent implements OnInit {

  

  constructor(private router: Router, private sharedService: SharedService, private authService: AuthenticationService) { }

  ngOnInit(): void {
  }

}
