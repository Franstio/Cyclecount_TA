import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lx17Filter } from '../Model/Lx17Filter.model';
import { LoginPlantModel } from '../Model/Login.model';
import { Store } from '@ngrx/store';
import { firstValueFrom, interval, lastValueFrom, mergeMap, tap } from 'rxjs';
import { Lx17Model } from '../Model/Lx17.model';
import { LICCFilter } from '../Model/LICCFilter.model';
import { LICCModel } from '../Model/LICC.model';

@Injectable({
  providedIn: 'root'
})
export class LICCService {

  constructor(private client: HttpClient,private plantStore: Store<{plant: LoginPlantModel}>) {
  }

  FetchLicc(count : number,liccFilter: LICCFilter )
  {
    return this.plantStore.select(x=>x.plant).pipe(mergeMap(
      res=>this.client.get(`LICC/ReadLICC/${count}?plantid=${res.Id}&dFrom=${liccFilter.dFrom}&dTo=${liccFilter.dTo}&werks=${res.WERKS}&lgnum=${res.LGNUM}&classFrom=${liccFilter.classFrom}&classTo=${liccFilter.classTo}&matnr=${liccFilter.search}`)
    ));
  }
  GetLicc(page:number,pagesize:number,search: string,lgtyp: string)
  {
    return this.plantStore.select(x=>x.plant).pipe(mergeMap(
      res => this.client.get(`LICC/${res.Id}?lgtyp=${lgtyp}&page=${page}&pagesize=${pagesize}&search=${search}`)
    ));
  }
  GetLgTypes()
  {
    return this.plantStore.select(x=>x.plant).pipe(mergeMap(
      res=> this.client.get(`LICC/Lgtypes/${res.Id}`)
    ));
  }
  CreateInventory(items : LICCModel[])
  {
    return  this.client.post("Lx17/CreateInventoryFromLicc",items);
  }
}
