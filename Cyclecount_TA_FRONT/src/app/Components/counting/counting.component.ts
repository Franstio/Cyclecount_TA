import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { lICCFilter } from 'src/app/Model/LICCFilter.model';
import { Lx17Model } from 'src/app/Model/Lx17.model';
import { Lx17Filter } from 'src/app/Model/Lx17Filter.model';
import { PaginationModel } from 'src/app/Model/Pagination.model';
import { Lx17Service } from 'src/app/Service/lx17.service';
import Swal from 'sweetalert2';
import { CountModalComponent } from '../count-modal/count-modal.component';
import { LICCService } from 'src/app/Service/liccService';
import { LiccModalComponent } from '../licc-modal/licc-modal.component';
import { Subject, combineLatestWith, defer, finalize, map, pipe, skip, take, takeUntil, takeWhile, tap } from 'rxjs';
import { Store } from '@ngrx/store';
import { isLoading, startLoading, stopLoading } from 'src/app/Store/LoadingStore/loading.action';

@Component({
  selector: 'app-counting',
  templateUrl: './counting.component.html',
  styleUrls: ['./counting.component.css']
})
export class CountingComponent implements OnInit,OnDestroy {
  Counting: PaginationModel<Lx17Model> = new PaginationModel();
  ReCounting: PaginationModel<Lx17Model> = new PaginationModel();
  ReCountFilter = { page: 1, pagesize: 10, Search: "" } as Lx17Filter;
  CountFilter = { page: 1, pagesize: 10, Search: "" } as Lx17Filter;
  ReCountTtlPage = 0;
  CountTtlPage = 0;
  show: boolean = false;
  complete: boolean = false;
  liccFilter = lICCFilter;
  today = "";
  ExitSubject = new Subject();
  constructor(private lxService: Lx17Service, private liccService: LICCService, private dtPipe: DatePipe, private modalService: NgbModal, private isLoadingStore: Store<{ isLoading: Boolean }>) {
    let n = new Date(new Date().getTime() + (1000 * 60 * 60 * 24));
    this.today = dtPipe.transform(new Date(), "yyyy-MM-dd") ?? "";
    this.liccFilter.dFrom = this.today;
    this.liccFilter.dTo = dtPipe.transform(n, "yyyy-MM-dd") ?? "";
  }
  ngOnDestroy(): void {
    this.ExitSubject.next(null);
    this.ExitSubject.complete();
    this.ExitSubject.unsubscribe();
  }

  ngOnInit(): void {
    this.lxService.SyncLX17().pipe(takeUntil(this.ExitSubject)).subscribe(res => {
      this.show = true;
      this.complete = true;
    });
    this.lxService.GetCountData(this.CountFilter).pipe(skip(1), takeUntil(this.ExitSubject)).subscribe(
      res => {
        this.Counting = <PaginationModel<Lx17Model>>res;
        this.CountTtlPage = Math.ceil(this.Counting.totalData / this.CountFilter.pagesize);
      }
    );
    this.lxService.GetReCountData(this.ReCountFilter).pipe(skip(1),takeUntil(this.ExitSubject)).subscribe(
      res => {
        this.ReCounting = <PaginationModel<Lx17Model>>res;
        this.ReCountTtlPage = Math.ceil(this.ReCounting.totalData / this.ReCountFilter.pagesize);
      }
    )
    defer(()=>{
      this.isLoadingStore.dispatch(startLoading());
    return this.lxService.GetCountData(this.CountFilter).pipe(takeUntil(this.ExitSubject),map(x=>(<PaginationModel<Lx17Model>>x))).pipe(
      combineLatestWith(
        this.lxService.GetReCountData(this.ReCountFilter).pipe(takeUntil(this.ExitSubject),map(x=>(<PaginationModel<Lx17Model>>x)))
      )
    );}).pipe(take(1),tap(x=>this.isLoadingStore.dispatch(stopLoading()))).subscribe(x=>{
      this.Counting = x[0];
      this.CountTtlPage = Math.ceil(x[0].totalData/this.CountFilter.pagesize);
      this.ReCounting = x[1];
      this.ReCountTtlPage = Math.ceil(x[1].totalData/this.ReCountFilter.pagesize);
    })
  }
  setPage(page: number, type: 'Count' | 'ReCount') {
    if (type == 'Count') {
      this.CountFilter.page = page;
      this.lxService.GetCountData(this.CountFilter).pipe(takeUntil(this.ExitSubject)).subscribe(
        res => {
          this.Counting = <PaginationModel<Lx17Model>>res;
          this.CountTtlPage = Math.ceil(this.Counting.totalData / this.CountFilter.pagesize);
        }
      );
    }
    else (type == 'ReCount')
    {
      this.ReCountFilter.page = page;
      this.lxService.GetReCountData(this.ReCountFilter).pipe(takeUntil(this.ExitSubject)).subscribe(
        res => {
          this.ReCounting = <PaginationModel<Lx17Model>>res;
          this.ReCountTtlPage = Math.ceil(this.ReCounting.totalData / this.ReCountFilter.pagesize);
        }
      )
    }
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

  showCountModal(model: Lx17Model, type: 'Count' | 'ReCount') {
    let ref = this.modalService.open(CountModalComponent, { size: "xl" });
    ref.componentInstance.material = model;
    ref.componentInstance.type = type;
    ref.result.then(x=>{
      this.setPage(1,type);
    });
  }
  openLicc() {
    defer(() => {
      this.isLoadingStore.dispatch(isLoading({status:true}));
      return this.liccService.FetchLicc(this.liccFilter.count, this.liccFilter);
    }).pipe(take(1)).subscribe(x => {
      this.isLoadingStore.dispatch(isLoading({status:false}));
      let ref = this.modalService.open(LiccModalComponent, { size: "xl" });
    });
  }
}
