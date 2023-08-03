import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lx17Filter } from '../Model/Lx17Filter.model';
import { LoginPlantModel } from '../Model/Login.model';
import { Store } from '@ngrx/store';
import { defer, firstValueFrom, interval, lastValueFrom, mergeMap, tap } from 'rxjs';
import { Lx17Model } from '../Model/Lx17.model';
import { startLoading, stopLoading } from '../Store/LoadingStore/loading.action';

@Injectable({
  providedIn: 'root'
})
export class Lx17Service {

  constructor(private client: HttpClient,private plantStore: Store<{plant: LoginPlantModel}>,private loadingStore: Store<{isLoading:{satus:boolean}}>) { }

  GetCountData(filter: Lx17Filter)
  {
    return this.plantStore.select(x=>x.plant).pipe(
      mergeMap(val=>
        this.client.get(`Lx17/Count?plant=${val.WERKS}&page=${filter.page}&pagesize=${filter.pagesize}&search=${filter.Search}`)
      )
    );
  }
  SyncLX17()
  {
    return this.plantStore.select(x=>x.plant).pipe(
      mergeMap(val=> this.client.get(`Lx17/SyncLx17?lgnum=${val.LGNUM}&werks=${val.WERKS}`))
    );
  }
  GetReCountData(filter: Lx17Filter)
  {
    return this.plantStore.select(x=>x.plant).pipe(
      mergeMap(val=>
        this.client.get(`Lx17/ReCount?plant=${val.WERKS}&page=${filter.page}&pagesize=${filter.pagesize}&search=${filter.Search}`)
      )
    );
  }
  Counting(lx17 : Lx17Model)
  {
    return defer(()=>{
      this.loadingStore.dispatch(startLoading());
      return this.client.post(`Lx17/Count`,[lx17]);
    }).pipe(tap(_=>this.loadingStore.dispatch(stopLoading())));
  }

  ReCounting(lx17 : Lx17Model)
  {
    return defer(()=>{
      this.loadingStore.dispatch(startLoading());
      return this.client.post(`Lx17/ReCount`,[lx17]);
    }).pipe(tap(_=>this.loadingStore.dispatch(stopLoading())));
  }
  GetLx17Report(filter: Lx17Filter)
  {
    return this.plantStore.select(x=>x.plant).pipe(
      mergeMap(
        p => this.client.get(`Lx17/${p.WERKS}?page=${filter.page}&pagesize=${filter.pagesize}&dFrom=${filter.dFrom}&dTo=${filter.dTo}&search=${filter.Search}`)
      ),
    );
  }
  ToggleRecount(Lx17 : Lx17Model[])
  {
    return this.client.put("Lx17/Recount",Lx17);
  }
  GetLx17Log(filter : Lx17Filter)
  {
    return this.plantStore.select(x=>x.plant).pipe(
      mergeMap(
        p => this.client.get(`Lx17/Log/${p.Id}?page=${filter.page}&pagesize=${filter.pagesize}&dFrom=${filter.dFrom}&dTo=${filter.dTo}&search=${filter.Search}`)
      )
    );
  }
}
