using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModles.DTO.Response.Auth
{
    public class LoginUserResponse
    {
        public string JwtToken { get; set; } = null!;
    }
}
