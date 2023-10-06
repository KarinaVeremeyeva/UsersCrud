using System.ComponentModel.DataAnnotations;

namespace UsersCrud.Api.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
