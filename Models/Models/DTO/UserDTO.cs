using System.ComponentModel.DataAnnotations;

namespace Models.Models.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public IList<string>? Roles { get; set; }
    }
}
