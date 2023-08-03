import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { CountingComponent } from './Components/counting/counting.component';
import { Lx17ReportComponent } from './Components/lx17-report/lx17-report.component';
import { CountingHistoryComponent } from './Components/counting-history/counting-history.component';
import { AuthGuard } from './Guard/auth.guard';

const routes: Routes = [
  {path: "",pathMatch:'full',redirectTo:'count'},
  {path:"login",component:LoginComponent},
  {path:"count",component:CountingComponent,canActivate: [AuthGuard]},
  {path:"lx17report",component:Lx17ReportComponent,canActivate: [AuthGuard]},
  {path: 'count-history',component: CountingHistoryComponent,canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
