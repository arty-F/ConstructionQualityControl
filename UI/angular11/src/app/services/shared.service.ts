import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs'
import { CityReadDto } from 'src/app/dtos/city/city-read-dto'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { environment } from 'src/environments/environment'
import { OrderCreateDto } from '../dtos/order/order-create-dto'
import { OrderRootReadDto } from '../dtos/order/order-root-read-dto'
import { WorkReadDto } from '../dtos/order/work-read-dto'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  creatingOrder: OrderCreateDto
  viewedWork: OrderRootReadDto

  constructor(private http: HttpClient) { }

  AddUser(user: UserCreateDto): Observable<UserCreateDto> {
    return this.http.post<UserCreateDto>(environment.apiUrl + '/User', user, httpOptions)
  }

  AddOrder(): Observable<OrderCreateDto> {
    return this.http.post<OrderCreateDto>(environment.apiUrl + '/Order', this.creatingOrder, httpOptions)
  }

  GetOrders(): Observable<OrderRootReadDto[]> {
    return this.http.get<OrderRootReadDto[]>(environment.apiUrl + '/Order', httpOptions)
  }

  GetWorksOfCity(city: CityReadDto): Observable<OrderRootReadDto[]> {
    return this.http.get<OrderRootReadDto[]>(environment.apiUrl + '/Order/Work/City/' + city.id, httpOptions)
  }

  GetWorks(): Observable<OrderRootReadDto[]> {
    return this.http.get<OrderRootReadDto[]>(environment.apiUrl + '/Order/Work', httpOptions)
  }

  GetViewedWorkInfo(): Observable<WorkReadDto> {
    return this.http.get<WorkReadDto>(environment.apiUrl + '/Order/Work/' + this.viewedWork.id, httpOptions)
  }

  GetFormatedDate(dateStr: Date): string {

    let date = new Date(dateStr)
    let year = date.getFullYear().toString()
    let month = (date.getMonth() + 1).toString()
    if (month.length == 1) {
      month = '0' + month
    }
    let day = date.getDate().toString()
    if (day.length == 1) {
      day = '0' + day
    }
    return day + '.' + month + '.' + year
  }
}
