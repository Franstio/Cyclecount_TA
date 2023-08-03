import { Injectable, ɵresetJitOptions } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, concat, concatMap, first, flatMap, mergeMap, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Store } from '@ngrx/store';
import { LoginModel } from '../Model/Login.model';
import Swal from 'sweetalert2';
import { stopLoading } from '../Store/LoadingStore/loading.action';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private store: Store<{auth: LoginModel}>,private loadingStore: Store<{isLoadingStore: {isLoading: boolean}}>,private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let r = environment.url;
    const apiReq = request.clone({url: `${environment.url}/${request.urlWithParams}`});
    return this.store.select(state=>state.auth.token).pipe(
      first(),
      concatMap(
        z => {
          if (z)
          {
            let clone = apiReq.clone({headers: apiReq.headers.set("Authorization",`Bearer ${z}`)});
            return next.handle(clone).pipe(catchError((err: HttpErrorResponse)=>{
              this.loadingStore.dispatch(stopLoading());
              if (err.status == 500)
              {
                Swal.fire({
                  title:"Error!",
                  text: err.error,
                  icon:'error'
                });
              }
              else if (err.status == 401)
              {
                this.router.navigate(['login']);
              }
              return throwError(()=>err);
            }));
          }
          return next.handle(apiReq);
        }
      )
    );
  }
}
