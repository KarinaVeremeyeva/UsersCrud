namespace UsersCrud.DAL.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public List<Role> Roles { get; set; } = new();
    }
}