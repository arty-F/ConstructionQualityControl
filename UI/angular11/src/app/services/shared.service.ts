import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { from, Observable, of, throwError } from 'rxjs'
import { CityReadDto } from 'src/app/dtos/city/city-read-dto'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { JSONwebToken } from 'src/app/dtos/jwt/jwt'
import { environment } from 'src/environments/environment'

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

  AddCity(item: any) {
    return this.http.post(environment.apiUrl + '/City', item)
  }

  UpdateCity(item: any) {
    return this.http.put(environment.apiUrl + '/City', item)
  }

  DeleteCity(item: any) {
    return this.http.delete(environment.apiUrl + '/City', item)
  }

  AddUser(user: UserCreateDto): Observable<UserCreateDto> {
    return this.http.post<UserCreateDto>(environment.apiUrl + '/User', user)
  }
}
