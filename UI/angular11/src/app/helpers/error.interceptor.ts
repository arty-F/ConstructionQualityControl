import { Injectable } from '@angular/core'
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { catchError } from 'rxjs/operators'
import { Router } from '@angular/router'
import { environment } from 'src/environments/environment'

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                catchError(err => {
                    if (err.status === 401) {
                        if (this.router.url !== environment.apiUrl + '/Auth') {
                            this.router.navigate(['Auth'])
                        }
                        else {
                            location.reload(true)
                        }
                    }
                    return throwError(err)
                })
            )
    }
}