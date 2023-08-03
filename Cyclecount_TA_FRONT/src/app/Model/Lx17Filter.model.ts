export interface Lx17Filter{
  dFrom : string,
  dTo : string ,
  Search: string ,
  Lgtyp: string,
  page: number,
  pagesize:number
}
export const lx17Filter = {
 dFrom: new Date().toDateString(),
 dTo: new Date().toDateString(),
 Search : "",
 Lgtyp: "",
 page : 1,
 pagesize:10
} as Lx17Filter
