import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { JSONwebToken } from 'src/app/dtos/jwt/jwt'
import { environment } from 'src/environments/environment';
import { personalData } from '../models/personal-data';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  login(login: string, password: string) {
    const url = environment.apiUrl + '/Authentication?login=' + login
    return this.http.post<JSONwebToken>(url, JSON.stringify(password), httpOptions)
      .pipe(
        map(resp => {
          localStorage.setItem(personalData.AccessToken, resp.access_Token)
          localStorage.setItem(personalData.UserName, resp.userName)
        })
      )
  }

  // !!!!! TODO returns correct bool value !!!!!
  checkTokenExpiration() {
    const url = environment.apiUrl + '/Authentication'
    return this.http.get(url, httpOptions)
      .pipe(() => {
        catchError(err => {
          console.log('ret false')
          return of(false)
        })
        console.log('ret true')
        return of(true)
      }
      )
  }

  logout() {
    localStorage.removeItem(personalData.AccessToken);
    localStorage.removeItem(personalData.UserName);
  }

  getToken(): string {
    return localStorage.getItem(personalData.AccessToken)
  }

  getUserName(): string {
    return localStorage.getItem(personalData.UserName)
  }
}
