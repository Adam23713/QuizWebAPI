using AutoMapper;
using Azure.Core;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models.DTO;
using Models.Requests.Auth;
using Models.Response.Auth;
using System.Data;
using static Azure.Core.HttpHeader;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenRepository tokenRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            IList<UserDTO> userDTOList = new List<UserDTO>();
            var users = await userManager.Users.ToListAsync();

            foreach (var user in users) 
            {
                userDTOList.Add(ConvertIdentityUSerToDTO(user));
            }

            return Ok(userDTOList);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var usernDTO = ConvertIdentityUSerToDTO(user);

            return Ok(usernDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string id, [FromBody] UpdateUserRequest request)
        {
            //TODO: If password or Role update failed revet changes
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                var checkCurrentPassword = await userManager.CheckPasswordAsync(user, request.CurrentPassword);
                var checkNewPassword = await userManager.CheckPasswordAsync(user, request.NewPassword);
                if (checkCurrentPassword)
                {
                    if(!user.Email.Equals(request.UserName, StringComparison.OrdinalIgnoreCase)) //Email change
                    {
                        var existsUser = await userManager.FindByEmailAsync(request.UserName);
                        if(existsUser != null)
                        {
                            return Conflict("Email already used");
                        }
                    }

                    //Update properties
                    user.UserName = request.UserName;
                    user.Email = request.UserName;
                    
                    //Update password if need
                    if(!checkNewPassword)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);
                        if (result != null && !result.Succeeded)
                        {
                            return BadRequest(result);
                        }
                    }

                    //Update roles if need
                    if(request.Roles != null)
                    {
                        await UpdateRoles(user, request.Roles);
                    }

                    await userManager.UpdateAsync(user);
                    return Ok("User updated");
                }
                else 
                {
                    return Forbid("Current password incorect");
                }
            }
            return NotFound("User not found");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string id)
        {
            // Find the user by ID
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }

            // Delete the user
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User deleted");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        #region Helpers

        private async Task UpdateRoles(IdentityUser user, List<string> newRoles) 
        {
            // Remove all current roles
            var currentRoles = await userManager.GetRolesAsync(user);
            foreach (var role in currentRoles)
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }

            // Add new roles
            foreach (var role in newRoles)
            {
                // Check if the role exists, if not, create it
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                await userManager.AddToRoleAsync(user, role);
            }
        }

        private UserDTO ConvertIdentityUSerToDTO(IdentityUser user)
        {
            var roles = userManager.GetRolesAsync(user);
            roles.Wait();
            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.Email,
                Password = "****",
                Roles = roles.Result
            };
        }

        #endregion

    }
}
