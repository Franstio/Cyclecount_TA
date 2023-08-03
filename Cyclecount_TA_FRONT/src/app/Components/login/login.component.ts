import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subscription, defer, subscribeOn, tap } from 'rxjs';
import { AuthService } from 'src/app/Service/auth.service';
import { startLoading, stopLoading } from 'src/app/Store/LoadingStore/loading.action';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit,OnDestroy {
  fg : FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('',Validators.required)
  });
  subs : Subscription = new Subscription();
  constructor(private authService: AuthService,private router: Router,private store: Store) { }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
  }
  Login()
  {
    this.subs = defer(()=>
    {
      this.store.dispatch(startLoading());
      return this.authService.Login(this.fg.value);
    }).pipe(tap({complete: ()=>this.store.dispatch(stopLoading()),error:(err)=>this.store.dispatch(stopLoading())})).subscribe({
      next:(res)=>{
        Swal.fire({
          title:"Login Success",
          icon:"success",
        }).then((x)=>{
          this.router.navigate(['count']);
        }
        );
      },
      error: ()=>{
        Swal.fire({
          title:"Authentication Failed",
          icon:'error'
        })
      }
    });
  }

}
