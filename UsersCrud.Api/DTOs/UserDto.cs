namespace UsersCrud.Api.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
