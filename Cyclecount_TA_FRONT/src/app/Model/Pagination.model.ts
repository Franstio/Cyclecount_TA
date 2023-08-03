export class PaginationModel<T>{
  public data: T[] = []  ;
  public totalData : number = 0;
  public pageNum : number = 0;
  public firstPage: boolean =true;
  public lastPage: boolean = false;
}
