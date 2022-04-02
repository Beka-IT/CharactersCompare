using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        IUserService userService;
        readonly ITokenService tokenService;
        AppDbContext context;

        public AuthController(AppDbContext context,IUserService userService,ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.context = context;
        }
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]User userPost)
        {
            if (userPost == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = userService.GetAllUsers()
                .FirstOrDefault(u => (u.Login == userPost.Login) &&
                                        (u.Password == userPost.Password));
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userPost.Login),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();
            user.LoginModel.RefreshToken = refreshToken;
            user.LoginModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await context.SaveChangesAsync();
            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
        [HttpPost]
        public async Task<ActionResult<User>> Authorizate(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            var users = await userService.GetAllUsersAsync();
            var realUser = users.FirstOrDefault(u=>u.Login == user.Login);
            if(realUser == null || realUser.Password != user.Password)
            {
                return Unauthorized();
            }
            return Ok();
            
        }
        
    }
}