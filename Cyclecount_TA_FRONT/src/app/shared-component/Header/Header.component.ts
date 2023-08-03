import { AfterContentInit, AfterViewChecked, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { EMPTY, Observable, Subject, Subscribable, Subscription, filter, first, forkJoin, iif, isEmpty, map, merge, mergeMap, of, startWith, switchMap, take, takeUntil, tap } from 'rxjs';
import { LoginModel, LoginPlantModel } from 'src/app/Model/Login.model';
import { AuthService } from 'src/app/Service/auth.service';
import { setPlant } from 'src/app/Store/PlantStore/plant.action';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-Header',
  templateUrl: './Header.component.html',
  styleUrls: ['./Header.component.css']
})
export class HeaderComponent implements OnInit,OnDestroy,AfterViewChecked {
  data$: Observable<LoginModel> ;
  isLogin$ : Observable<boolean>;
  plants: LoginPlantModel[] = [];
  plantSubs : Subscription = new Subscription();
  subject = new Subject();
  selectedPlant: LoginPlantModel = {} as LoginPlantModel;
  constructor(private authService: AuthService,private store: Store<{auth: LoginModel}>,private plantStore : Store<{plant:LoginPlantModel}>,private router : Router) {
    this.data$ = store.select(state=>state.auth);
    this.data$.subscribe(x=>{this.plants = x.plant});
    this.isLogin$ = store.select(state=>state.auth.userid).pipe(map(x=>x!=null));
    this.plantStore.select(x=>x.plant).subscribe(x=>{
        this.selectedPlant =x;
    });
  }
  ngAfterViewChecked(): void {
    this.plantSubs = this.plantStore.select(x=>x.plant).pipe(switchMap(val=>
      iif(()=>val == undefined || val == null,this.store.select(s=>s.auth).pipe(map(x=>x.plant[0])),of(val) )
      ),takeUntil(this.subject)).subscribe(x=>{
        let s = this.plants.find(z=>z.Id==x.Id);
        if (s == undefined)
          return;
        this.selectedPlant=s;
        this.subject.next(null);
        this.subject.complete();
      });
  }
  ngOnDestroy(): void {
    this.plantSubs.unsubscribe();
  }

  ngOnInit() {
  }
  updatePlant(id : number)
  {
    let find = this.plants.find(x=>x.Id==id);
    if (find == undefined)
      return;
    this.plantStore.dispatch(setPlant({plant: find}));
  }
  Logout()
  {
    this.authService.Logout();
    Swal.fire({
      title:"Logout Success",
      icon:"info"
    }).then(x=>{this.router.navigateByUrl("/login")});
  }

}
