import { createReducer, on } from "@ngrx/store";
import { isLoading, startLoading, stopLoading } from "./loading.action";

export const initialState = {status:false};
export const isLoadingReducer = createReducer(initialState,
on(isLoading,(state,newState)=>{
   return newState;
  }),
  on(startLoading,(state)=>({status:true})),
  on(stopLoading,(state)=>({status:false}))
);
