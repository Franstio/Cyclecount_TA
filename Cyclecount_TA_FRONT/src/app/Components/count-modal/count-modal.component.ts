import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Lx17Model } from 'src/app/Model/Lx17.model';
import { Lx17Service } from 'src/app/Service/lx17.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-count-modal',
  templateUrl: './count-modal.component.html',
  styleUrls: ['./count-modal.component.css']
})
export class CountModalComponent implements OnInit {
  @Input() material! : Lx17Model ;
  @Input() type : 'Count' | 'ReCount' = 'Count';
	constructor(public activeModal: NgbActiveModal,private lx17Service: Lx17Service) {}
  ngOnInit(): void {

  }
  Count()
  {
    this.material.menge = this.material.menga;
    this.material.plant = null;
    if (this.type == 'Count')
    {
    this.lx17Service.Counting(this.material).subscribe(res=>{
      Swal.fire({
        titleText: `Success Counting ${this.material.ivnum}`,
        icon:"success"
      }).then(res=>{this.activeModal.close()})
    });
    }
    else if (this.type=='ReCount')
    {
      this.lx17Service.ReCounting(this.material).subscribe(res=>{
        Swal.fire({
          titleText: `Success Counting ${this.material.ivnum}`,
          icon:"success"
        }).then(res=>this.activeModal.close())
      });

    }
  }
}
