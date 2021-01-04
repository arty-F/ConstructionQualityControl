import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthResult } from 'src/app/dtos/jwt/auth-result'
import { environment } from 'src/environments/environment';
import { UserReadDto } from '../dtos/user/user-read-dto';
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

  public user: UserReadDto

  constructor(private router: Router, private http: HttpClient) {
    let token = localStorage.getItem(personalData.AccessToken)
    if (token != null) {
      this._token.next(token)
      const url = environment.apiUrl + '/Authentication'
      this.http.get<UserReadDto>(url, httpOptions).subscribe(resp => {
        this.setUserData(resp)
        this.router.navigate(['Orders'])
      })
    }
  }

  login(login: string, password: string) {
    const url = environment.apiUrl + '/Authentication?login=' + login
    return this.http.post<AuthResult>(url, JSON.stringify(password), httpOptions)
      .pipe(
        map(resp => {
          this.user = resp.user
          localStorage.setItem(personalData.AccessToken, resp.access_Token)
          this._token.next(resp.access_Token)
          this.setUserData(resp.user)
        })
      )
  }

  logout() {
    this.user = null
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

  private setUserData(user: UserReadDto) {
    localStorage.setItem(personalData.UserName, user.login)
    localStorage.setItem(personalData.UserRole, user.role)
    this._name.next(user.login)
    this._role.next(user.role)
  }
}
