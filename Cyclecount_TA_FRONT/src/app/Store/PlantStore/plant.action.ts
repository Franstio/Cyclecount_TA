import { createAction, props } from '@ngrx/store';
import { LoginModel, LoginPlantModel } from 'src/app/Model/Login.model';

export const setPlant = createAction('[Auth Service] Set Plant', props<{plant: LoginPlantModel}>());
