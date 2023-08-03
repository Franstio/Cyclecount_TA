import { createReducer, on } from '@ngrx/store';
import { login,logout } from './auth.action';
import { LoginModel, LoginPlantModel, LoginRoleModel } from 'src/app/Model/Login.model';

export const initialState :LoginModel = {
  userid : null,username: "",name: "",role: {} as LoginRoleModel,token :"",plant: []
};

export const authReducer = createReducer(
  initialState,
  on(login, (state,newState) => newState),
  on(logout, (state) => ({...initialState,Idusin:0,Idusup:0})),
);
