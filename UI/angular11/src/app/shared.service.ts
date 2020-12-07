import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { from, Observable, of, throwError } from 'rxjs'
import { CityReadDto } from 'src/app/dtos/city/city-read-dto'
import { UserCreateDto } from 'src/app/dtos/user/user-create-dto'
import { JSONwebToken } from 'src/app/dtos/jwt/jwt'


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = 'http://localhost:50970'

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }

  GetCityList(): Observable<CityReadDto[]> {
    return this.http.get<CityReadDto[]>(apiUrl + '/City')
  }

  AddCity(item: any) {
    return this.http.post(apiUrl + '/City', item)
  }

  UpdateCity(item: any) {
    return this.http.put(apiUrl + '/City', item)
  }

  DeleteCity(item: any) {
    return this.http.delete(apiUrl + '/City', item)
  }

  AddUser(user: UserCreateDto): Observable<UserCreateDto> {
    return this.http.post<UserCreateDto>(apiUrl + '/User', user)
  }

  Authenticate(login: string, password: string): any {
    return this.http.post(apiUrl + '/Authentication/' + login, password)
  }
}
