import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-city',
  templateUrl: './show-city.component.html',
  styleUrls: ['./show-city.component.css']
})
export class ShowCityComponent implements OnInit {

  constructor(private service: SharedService) { }

  cityList: any = [];

  ngOnInit(): void {
    this.RefreshCityList();
  }

  RefreshCityList() {
    this.service.GetCityList().subscribe( data => {
      this.cityList = data;
    });
  }
}
