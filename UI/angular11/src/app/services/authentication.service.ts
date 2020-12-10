import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { JSONwebToken } from 'src/app/dtos/jwt/jwt'
import { environment } from 'src/environments/environment';

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
    return this.http.post<JSONwebToken>(url, JSON.stringify(password), httpOptions).pipe(map(resp => {
      localStorage.setItem('access_Token', resp.access_Token)
    }))
  }

  logout() {
    localStorage.removeItem('access_Token');
  }

  getToken(): string {
    return localStorage.getItem('access_Token')
  }
}
