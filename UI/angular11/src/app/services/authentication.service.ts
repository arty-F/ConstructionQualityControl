import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs';
import { JSONwebToken } from 'src/app/dtos/jwt/jwt'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = 'http://localhost:50970/Authentication?login='

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  login(login: string, password: string): Observable<boolean> {
    const resp = this.http.post<JSONwebToken>(apiUrl + login, JSON.stringify(password), httpOptions).subscribe(resp => {
      localStorage.setItem('access_Token', resp.access_Token)
      return of(true)
    })

    return of(false)
  }

  logout() {
    localStorage.removeItem('access_Token');
  }

  getToken(): string {
    return localStorage.getItem('access_Token')
  }
}
