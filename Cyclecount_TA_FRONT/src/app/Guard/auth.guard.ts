import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, filter, first, map, of, switchMap, tap } from 'rxjs';
import { LoginModel } from '../Model/Login.model';
import { logout } from '../Store/AuthStore/auth.action';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private store: Store<{auth: LoginModel}>,private router: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.store.select(state => state.auth.token).pipe(
      switchMap(x=>
        x != "" && x != undefined && x != null ? of(true) : of(false) ),
      tap(x=>{
        if (!x)
        {
          this.store.dispatch(logout());
          this.router.navigate(['login'])
        }
      })
    );
  }

}
