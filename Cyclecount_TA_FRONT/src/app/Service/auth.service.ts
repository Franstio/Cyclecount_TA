import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { login, logout } from '../Store/AuthStore/auth.action';
import { LoginModel } from '../Model/Login.model';
import { Buffer } from 'buffer';
import { setPlant } from '../Store/PlantStore/plant.action';
import { tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private client : HttpClient,private store: Store) { }

  public Login(loginModel : {Username: String,Password: String})
  {
    return this.client.post("Auth/Login",loginModel).pipe(
      tap((res)=>{
        let ret :any= res;
        let token : string = ret.response.toString();
        let data = Buffer.from(token.split(".")[1],"base64").toString();
        let json : any= JSON.parse(data);
        json.Role = JSON.parse(<string>json.Role);
        json.Depts = JSON.parse(<string>json.Depts);
        let loginModel = {
          userid: json.SesaId,
          username:json.Username,
          name : json.Name,
          plant: json.Depts,
          role: json.Role,
          token: token
        } as LoginModel;
        this.store.dispatch(login(loginModel));
        this.store.dispatch(setPlant({plant:loginModel.plant[0]}));
      })
    );
  }
  public Logout()
  {
    this.store.dispatch(logout());
  }
}
