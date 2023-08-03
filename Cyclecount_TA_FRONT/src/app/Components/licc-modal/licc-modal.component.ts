import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LICCModel } from 'src/app/Model/LICC.model';
import { LICCFilter, lICCFilter } from 'src/app/Model/LICCFilter.model';
import { PaginationModel } from 'src/app/Model/Pagination.model';
import { LICCService } from 'src/app/Service/liccService';
import { CountModalComponent } from '../count-modal/count-modal.component';
import { Lx17Model } from 'src/app/Model/Lx17.model';
import { defer } from 'rxjs';
import { Store } from '@ngrx/store';
import { isLoading, startLoading, stopLoading } from 'src/app/Store/LoadingStore/loading.action';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-licc-modal',
  templateUrl: './licc-modal.component.html',
  styleUrls: ['./licc-modal.component.css']
})
export class LiccModalComponent implements OnInit {
  LICC: PaginationModel<LICCModel> = new PaginationModel();
  LiccSelected : LICCModel[] = [];
  Lgtype : string = "";
  Lgtypes : string[] = [];
  Filter : LICCFilter = lICCFilter;
  LiccTotalPage = 0;
  constructor(private liccService: LICCService,private modalService : NgbModal,public activeModal : NgbActiveModal,private isLoadingStore: Store<{ isLoading: Boolean }>) {

  }

  ngOnInit(): void {
    this.Filter.page = 1;
    this.GetLicc();
  }

  BatchCreateInv()
  {
    let dump = this.LiccSelected;
    let subject = defer(()=>
      {
        this.isLoadingStore.dispatch(startLoading());
        return this.liccService.CreateInventory(this.LiccSelected);
      }
    ).subscribe(x=>{
      this.isLoadingStore.dispatch(stopLoading());
      let cnt = this.LiccSelected.length;
      this.LiccSelected = [];
      Swal.fire({
        title:`Create Inventory Number for ${cnt} Material(s)`,
        icon:"success",
      }).then(x=>this.setPage(1));
    });
  }

  DownloadExcel()
  {}
  StartCount(model : LICCModel)
  {
    this.liccService.CreateInventory([model]).subscribe(res=>{
      let s : any  =res;
      let ref = this.modalService.open(CountModalComponent,{size:"xl"});
      ref.componentInstance.material = (<Lx17Model[]>s)[0];
      ref.componentInstance.type = 'Count';
    });
  }
  GetLicc()
  {
    this.liccService.GetLgTypes().subscribe(x=>{this.Lgtypes=<string[]>x});
    this.liccService.GetLicc(this.Filter.page,this.Filter.pagesize,this.Filter.search,this.Lgtype).subscribe({
      next: (res) =>{
        let s :any = res;
        this.LICC = s;
        this.LiccTotalPage = Math.ceil(this.LICC.totalData / this.Filter.pagesize)
      }
    });
  }
  setPage(page: number)
  {
    this.Filter.page =page;
    this.GetLicc();
  }
  createRange(ttl:number)
  {
    let data = [];
    for (let i=0;i<ttl;i++)
      data.push(i+1);
    return data;
  }

  Select(licc : LICCModel)
  {
    let found = this.LiccSelected.find(x=>x.id==licc.id);
    if (found == undefined || found == null)
      this.LiccSelected.push(licc);
    else
      this.LiccSelected = this.LiccSelected.filter(x=>x.id!=found?.id);

  }
  week(date: string) {
    let DT = new Date(date);
    let _date : any= DT;
    let oneJan : any = new Date(_date.getFullYear(), 0, 1);
    var numberOfDays = Math.floor((_date - oneJan) / (24 * 60 * 60 * 1000));
    var result = Math.ceil((DT.getDay() + 1 + numberOfDays) / 7);
    return result;
  }
  CheckSelected(licc: LICCModel)
  {
    let found =  this.LiccSelected.find(x=>x.id==licc.id);
    return found != undefined && found != null && !found?.ttext.includes("Blocked") && !found?.ttext.includes("blocked");
  }
  BlkCheck(licc: LICCModel)
  {
    return licc.ttext.includes("Blocked") || licc.ttext.includes("blocked");
  }
  BtnCheck(data:LICCModel)
  {

    if (data.ttext.includes("Blocked") || data.ttext.includes("blocked"))
      return 'btn-secondary'
    else
      return 'btn-info'
  }
  DisBtnCheck(data: LICCModel)
  {
    return (data.ttext.includes("Blocked") || data.ttext.includes("blocked"));
  }
}
