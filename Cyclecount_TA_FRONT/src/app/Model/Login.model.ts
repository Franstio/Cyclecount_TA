

export interface LoginModel {
  userid: number | null, username: string, name: string, role: LoginRoleModel, token: string,
  plant: LoginPlantModel[]
}
export interface LoginRoleModel{
  Id: number | null, RoleName: string,Level: number
}
export interface LoginPlantModel {
  Id: number,DeptName: string,WERKS: string,LGNUM: string, Mltp: number
}
