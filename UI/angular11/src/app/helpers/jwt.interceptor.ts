import { Injectable } from '@angular/core'
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Observable } from 'rxjs'

import { AuthenticationService } from 'src/app/services/authentication.service'

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private _token: string

    constructor(private authService: AuthenticationService) {
        authService.getToken().subscribe(res =>
            this._token = res
        )
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
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