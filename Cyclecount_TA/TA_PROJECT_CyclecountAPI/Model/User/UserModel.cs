namespace TA_PROJECT_CyclecountAPI.Model.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string SESAID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int DefaultDeptId { get; set; }
        public string Level { get; set; } = string.Empty;
        public int? RoleId { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Last_Updated { get; set; } = DateTime.Now;
        public virtual DeptModel DefaultDept { get; set; } = new DeptModel();
        public virtual RoleModel? Role { get; set; } 
        public virtual List<UserDeptModel> Depts { get; set; } = new List<UserDeptModel>();
    }
}
