import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { defer, tap } from 'rxjs';
import { Lx17Model } from 'src/app/Model/Lx17.model';
import { Lx17Filter, lx17Filter } from 'src/app/Model/Lx17Filter.model';
import { PaginationModel } from 'src/app/Model/Pagination.model';
import { Lx17Service } from 'src/app/Service/lx17.service';
import { startLoading, stopLoading } from 'src/app/Store/LoadingStore/loading.action';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lx17-report',
  templateUrl: './lx17-report.component.html',
  styleUrls: ['./lx17-report.component.css']
})
export class Lx17ReportComponent implements OnInit {
  Lx17Data : PaginationModel<Lx17Model> = new PaginationModel();
  Filter: Lx17Filter = lx17Filter;
  Lx17TtlPage = 0;
  abs = Math.abs;
  SelectedLx17 : Lx17Model[] = [];
  constructor(private service : Lx17Service,dPipe : DatePipe,private store: Store) {
    this.Filter = {...this.Filter,dFrom : dPipe.transform(this.Filter.dFrom,"yyyy-MM-dd") ?? "",dTo: dPipe.transform(new Date(new Date().getTime() + (1000 * 60 * 60 * 24)),"yyyy-MM-dd") ?? "" }
    this.setPage(1);
  }
  setPage(page : number)
  {
    this.Filter.page = page;
    defer(()=>{
      this.store.dispatch(startLoading());
      return this.service.GetLx17Report(this.Filter);
    }).pipe(tap(x=>this.store.dispatch(stopLoading()))).subscribe(x=>{
      this.Lx17Data = <PaginationModel<Lx17Model>>x;
      this.Lx17TtlPage = Math.ceil(this.Lx17Data.totalData/this.Filter.pagesize);
    });
  }
  ngOnInit(): void {
  }
  Recount()
  {
    this.service.ToggleRecount(this.SelectedLx17).subscribe(res=>{
      Swal.fire({
        title:"Success Recount",
        text: `Success Moving ${this.SelectedLx17.length} Material(s) To Recount`
      }).then(x=>{
        this.SelectedLx17 = [];
        this.setPage(1);
      })
    });
  }
  public badgechk(any: any) {
    if (any > 0)
      return 'bg-danger'
    else return 'bg-secondary'
  }
  createRange(ttl: number) {
    let data = [];
    for (let i = 0; i < ttl; i++)
      data.push(i + 1);
    return data;
  }

  getprice(lx : Lx17Model) {
    if (lx.vprsv == 'S')
      return lx.stprs / lx.peinh;
    else
      return lx.verpr / lx.peinh;
  }
  chkchange(lx: Lx17Model)
  {
    let found = this.SelectedLx17.find(x=>x.id==lx.id);
    if (found == undefined || found == null)
      this.SelectedLx17.push(lx);
    else
      this.SelectedLx17 = this.SelectedLx17.filter(x=>x.id!=lx.id);
  }
}
