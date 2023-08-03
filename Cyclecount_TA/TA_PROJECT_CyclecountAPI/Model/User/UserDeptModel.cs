namespace TA_PROJECT_CyclecountAPI.Model.User
{
    public class UserDeptModel
    {
        public int UserId { get; set; }
        public int DeptId { get; set; }
        public virtual UserModel User { get; set; } = new UserModel();
        public virtual DeptModel Dept { get; set; } = new DeptModel();
    }
}
