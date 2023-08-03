import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {
  loadingStatus : boolean = false;
  constructor(private store: Store<{isLoading: {status: boolean}}>) {
    this.store.select(x=>x.isLoading).subscribe(x=>{
      this.loadingStatus=x.status;
    });
   }

  ngOnInit(): void {
  }

}
