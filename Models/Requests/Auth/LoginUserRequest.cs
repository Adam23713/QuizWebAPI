﻿using System.ComponentModel.DataAnnotations;

namespace Models.Requests.Auth
{
    public class LoginUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
