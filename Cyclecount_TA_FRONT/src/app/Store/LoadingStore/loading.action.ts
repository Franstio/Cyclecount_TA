import { createAction, props } from "@ngrx/store";

export const isLoading = createAction('[Loading Service]',props<{status:boolean}>());
export const startLoading = createAction('[Start Loading]');
export const stopLoading  = createAction('[Stop Loading]');
