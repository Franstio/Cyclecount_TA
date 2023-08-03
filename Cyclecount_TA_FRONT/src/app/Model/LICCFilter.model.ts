export interface LICCFilter{
  plantId : number,
  page: number,
  pagesize:number,
  search: string,
  count : number,
  dTo: string,
  dFrom: string,
  classFrom :string,
  classTo:string
}
export const lICCFilter = {
  plantId : -1,
  page: 1,
  pagesize : 10,
  search: "",
  count: 100,
  dTo : new Date().toDateString(),
  dFrom: new Date().toDateString(),
  classTo: "B",
  classFrom: "A",
  werks:"",
  lgnum: "",
  matnr: ""
} as LICCFilter;
