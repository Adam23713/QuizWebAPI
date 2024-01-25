using System.ComponentModel.DataAnnotations;

namespace Models.Requests.Auth
{
    public class UpdateUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        public List<string>? Roles { get; set; }
    }
}
