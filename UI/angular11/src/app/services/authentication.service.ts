import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Observable, of, Subject } from 'rxjs';
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

  private _token = new Subject<string>()
  private _name = new Subject<string>()
  private _role = new Subject<string>()

  constructor(private http: HttpClient) { }

  login(login: string, password: string) {
    const url = environment.apiUrl + '/Authentication?login=' + login
    return this.http.post<JSONwebToken>(url, JSON.stringify(password), httpOptions)
      .pipe(
        map(resp => {
          localStorage.setItem(personalData.AccessToken, resp.access_Token)
          localStorage.setItem(personalData.UserName, resp.userName)
          localStorage.setItem(personalData.UserRole, resp.userRole)
          this._token.next(resp.access_Token)
          this._name.next(resp.userName)
          this._role.next(resp.userRole)
        })
      )
  }

  logout() {
    localStorage.removeItem(personalData.AccessToken)
    localStorage.removeItem(personalData.UserName)
    localStorage.removeItem(personalData.UserRole)
    this._token.next()
    this._name.next()
    this._role.next()
  }

  getToken(): Observable<string> {
    return this._token.asObservable()
  }

  getUserName(): Observable<string> {
    return this._name.asObservable()
  }

  getUserRole(): Observable<string> {
    return this._role.asObservable()
  }
}