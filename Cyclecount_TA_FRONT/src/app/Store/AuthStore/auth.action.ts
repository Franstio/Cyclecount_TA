import { createAction, props } from '@ngrx/store';
import { LoginModel } from 'src/app/Model/Login.model';

export const login = createAction('[Auth Service] Login', props<LoginModel>());
export const logout = createAction('[Auth Service] Logout');
