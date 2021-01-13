import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { from, Observable, of, throwError } from 'rxjs'
import { CityReadDto } from 'src/app/dtos/city/city-read-dto'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { environment } from 'src/environments/environment'
import { OrderCreateDto } from '../dtos/order/order-create-dto'
import { OrderRootReadDto } from '../dtos/order/order-root-read-dto'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  creatingOrder: OrderCreateDto

  constructor(private http: HttpClient) { }

  GetCityList(): Observable<CityReadDto[]> {
    return this.http.get<CityReadDto[]>(environment.apiUrl + '/City', httpOptions)
  }

  AddUser(user: UserCreateDto): Observable<UserCreateDto> {
    return this.http.post<UserCreateDto>(environment.apiUrl + '/User', user, httpOptions)
  }

  AddOrder(): Observable<OrderCreateDto> {
    return this.http.post<OrderCreateDto>(environment.apiUrl + '/Order', this.creatingOrder, httpOptions)
  }

  GetOrders(): Observable<OrderRootReadDto[]> {
    return this.http.get<OrderRootReadDto[]>(environment.apiUrl + '/Order', httpOptions)
  }

  GetOrdersOfCity(city: CityReadDto): Observable<OrderRootReadDto[]> {
    return this.http.get<OrderRootReadDto[]>(environment.apiUrl + '/Order/City/' + city.id, httpOptions)
  }
}
