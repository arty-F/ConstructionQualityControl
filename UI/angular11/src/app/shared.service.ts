import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly ApiUrl = "http://localhost:50970/";

  constructor(private http: HttpClient) { }

  GetCityList(): Observable<any[]> {
    return this.http.get<any>(this.ApiUrl + 'City');
  }

  AddCity(item: any) {
    return this.http.post(this.ApiUrl+'City', item);
  }

  UpdateCity(item: any) {
    return this.http.put(this.ApiUrl+'City', item);
  }

  DeleteCity(item: any) {
    return this.http.delete(this.ApiUrl+'City', item)
  }
}
