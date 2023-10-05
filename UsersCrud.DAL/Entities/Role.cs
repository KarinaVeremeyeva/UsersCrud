using UsersCrud.DAL.Enums;

namespace UsersCrud.DAL.Entities
{
    public class Role : Entity
    {
        public Roles Name { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
