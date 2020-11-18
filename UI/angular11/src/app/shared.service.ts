import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { CityReadDto } from './dtos/city/city-read-dto';
import { RegionReadDto } from './dtos/region/region-read-dto';


const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
const apiUrl = 'http://localhost:50970';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }

  GetCityList(): Observable<CityReadDto[]> {
    return this.http.get<CityReadDto[]>(apiUrl + '/City');
  }

  AddCity(item: any) {
    return this.http.post(apiUrl + 'City', item);
  }

  UpdateCity(item: any) {
    return this.http.put(apiUrl + 'City', item);
  }

  DeleteCity(item: any) {
    return this.http.delete(apiUrl + 'City', item)
  }
}
