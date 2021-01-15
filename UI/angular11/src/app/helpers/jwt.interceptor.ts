import { Injectable } from '@angular/core'
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Observable } from 'rxjs'
import { personalData } from '../models/personal-data'

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private _token: string

    constructor() { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this._token == null) {
            this._token = localStorage.getItem(personalData.AccessToken)
        }

        if (this._token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this._token}`
                }
            })
        }

        return next.handle(request);
    }
}