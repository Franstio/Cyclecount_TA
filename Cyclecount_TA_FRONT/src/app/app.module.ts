import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EffectsModule } from '@ngrx/effects';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthService } from './Service/auth.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './Interceptor/auth-interceptor.interceptor';
import { ActionReducer, ActionReducerMap, MetaReducer, StoreModule } from '@ngrx/store';
import { authReducer } from './Store/AuthStore/auth.reducer';
import { RouterModule } from '@angular/router';
import {HeaderComponent} from './shared-component/Header/Header.component';
import { plantReducer } from './Store/PlantStore/plant.reducer';
import { LoginComponent } from './Components/login/login.component';
import { LoginModel } from './Model/Login.model';
import { localStorageSync} from 'ngrx-store-localstorage';
import { CountingComponent } from './Components/counting/counting.component';
import { Lx17Service } from './Service/lx17.service';
import { DatePipe } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CountModalComponent } from './Components/count-modal/count-modal.component';
import { LiccModalComponent } from './Components/licc-modal/licc-modal.component';
import { LICCService } from './Service/liccService';
import { LoadingComponent } from './shared-component/loading/loading.component';
import { isLoadingReducer } from './Store/LoadingStore/loading.reducer';
import { Lx17ReportComponent } from './Components/lx17-report/lx17-report.component';
import { CountingHistoryComponent } from './Components/counting-history/counting-history.component';

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return localStorageSync({keys: ['auth','plant'],rehydrate:true})(reducer);
}
const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    CountingComponent,
    CountModalComponent,
    LiccModalComponent,
    LoadingComponent,
    Lx17ReportComponent,
    CountingHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    EffectsModule.forRoot([]),
    StoreModule.forRoot({auth: authReducer,plant: plantReducer,isLoading: isLoadingReducer},{metaReducers}),
    StoreDevtoolsModule.instrument({maxAge:16}),
    NgbModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AuthInterceptor
    },
    AuthService,
    Lx17Service,
    LICCService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
