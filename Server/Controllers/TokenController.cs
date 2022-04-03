using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        readonly IUserService userService;
        readonly ITokenService tokenService;

        public TokenController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService ;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = userService.GetAllUsers().SingleOrDefault(u => u.Login == username);

            if (user == null  || user.LoginModel.RefreshToken != refreshToken || user.LoginModel.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid data");
            }

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            user.LoginModel.RefreshToken = newRefreshToken;
            await userService.SaveChangesAsync();

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            var user = userService.GetAllUsers().SingleOrDefault(u => u.Login == username);
            if (user == null) return BadRequest();

            user.LoginModel.RefreshToken = null;

            await userService.SaveChangesAsync();
            return BadRequest();
        }
    }
}