import { Injectable } from '@angular/core'
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { catchError } from 'rxjs/operators'

import { AuthenticationService } from 'src/app/services/authentication.service'
import { Router } from '@angular/router'
import { environment } from 'src/environments/environment'

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthenticationService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                catchError(err => {
                    if (err.status === 401) {
                        this.authService.logout()
                        if (this.router.url !== environment.apiUrl + '/Auth') {
                            this.router.navigate(['Auth'])
                        }
                        else {
                            location.reload(true)
                        }
                    }

                    const error = err.error.message || err.statusText
                    return throwError(error)
                })
            )
    }
}