import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { LoginModel, LoginPlantModel } from './Model/Login.model';
import { Observable, isEmpty, map } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'cyclecount TA';
  isLogin$ : Observable<boolean>;
  loginData$: Observable<LoginModel>;
  constructor(private loginStore: Store<{auth: LoginModel}>,private plantStore: Store<{plant:LoginPlantModel}>)
  {
    this.loginData$ = this.loginStore.select(state=>state.auth);
    this.isLogin$ = this.loginStore.select(state=>state.auth.userid).pipe(map(x=>x!=null));
    this.isLogin$.subscribe(x=>{
      let s = x;
    });
  }
}
