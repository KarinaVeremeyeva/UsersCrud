namespace UsersCrud.BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public IEnumerable<RoleModel> Roles { get; set; }
    }
}
