using System.Linq;
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
        AppDbContext db;
        public AuthController(AppDbContext context,IUserService userService)
        {
            this.userService = userService;
            this.db = context;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Authorizate(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            var realUser = await db.Users.FirstOrDefaultAsync(u=>u.Login == user.Login);
            if(realUser == null || realUser.Password != user.Password)
            {
                return Unauthorized();
            }
            return Ok();
            
        }
        
    }
}