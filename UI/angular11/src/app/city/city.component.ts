import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { SharedService } from '../shared.service';
import { CityReadDto } from '../dtos/city/city-read-dto';
import { RegionReadDto } from '../dtos/region/region-read-dto';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'region.id', 'region.name'];
  data: CityReadDto[] = [];
  isLoadingResults = true;

  constructor(private shared: SharedService) { }

  ngOnInit(): void {
    this.shared.GetCityList().subscribe(
      res => {
        this.data = res;
      });
  }
}
