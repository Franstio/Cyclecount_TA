import { Lx17Model } from "./Lx17.model";

export interface Lx17LogModel extends Lx17Model{
  lx17id: number,
  logged_userid: string,
  logged_user : {Name:string,SESAID:string},
  counter : {Name:string,SESAID:string},
  ymd8log : Date,
  action : number
}
