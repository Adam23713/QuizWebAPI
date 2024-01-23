using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.BaseModles.DTO.Requests.Auth;
using Models.BaseModles.DTO.Response.Auth;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenRepository tokenRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.UserName);
            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
                if(checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwt = tokenRepository.CreateJWT(user, roles.ToList());
                        return Ok(new LoginUserResponse { JwtToken = jwt});
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
