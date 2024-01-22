using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Requests.Auth;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        { 
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if(identityResult.Succeeded)
            {
                if(request.Roles != null && request.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, request.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User created");
                    }
                }
            }
            
            
            return BadRequest("Can't create user");
        }
    }
}
