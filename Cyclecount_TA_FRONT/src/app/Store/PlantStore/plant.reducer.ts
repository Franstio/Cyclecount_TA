import { createReducer, on } from '@ngrx/store';
import { setPlant } from './plant.action';
import { LoginModel, LoginPlantModel } from 'src/app/Model/Login.model';

export const initialState :LoginPlantModel = {
  Id : -1,DeptName:"" ,Mltp: 1,LGNUM: "",WERKS :""
};

export const plantReducer = createReducer(
  initialState,
  on(setPlant,
    (state,{plant}) => plant)
);
