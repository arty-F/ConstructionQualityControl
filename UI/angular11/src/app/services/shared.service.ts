import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { from, Observable, of, throwError } from 'rxjs'
import { CityReadDto } from 'src/app/dtos/city/city-read-dto'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { environment } from 'src/environments/environment'
import { OrderCreateDto } from '../dtos/order/order-create-dto'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }

  GetCityList(): Observable<CityReadDto[]> {
    return this.http.get<CityReadDto[]>(environment.apiUrl + '/City')
  }

  AddUser(user: UserCreateDto): Observable<UserCreateDto> {
    return this.http.post<UserCreateDto>(environment.apiUrl + '/User', user)
  }

  AddOrder(order: OrderCreateDto): Observable<OrderCreateDto> {
    return this.http.post<OrderCreateDto>(environment.apiUrl + '/Order', order)
  }
}
