
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsersAsync()
        {
            
            return await userService.GetAllUsersAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var user = await userService.GetUserAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            var newUser = await userService.AddUserAsync(user);
            return Ok(newUser);
        }
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUserAsync(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            var updatedUser = await userService.UpdateUserAsync(user);
            if(updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserAsync(int id)
        {
            var deletedUser = await userService.DeleteUserAsync(id);
            if(deletedUser == null)
            {
                return NotFound();
            }
            return Ok(deletedUser);
        }
    }
}