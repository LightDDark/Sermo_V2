using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TryController : ControllerBase
    {
        private readonly UsersService _service;

        public TryController(UsersService service)
        {
            _service = service;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            User? user = await _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.Server = HttpContext.Request.Host.Value;

            bool? res = await _service.AddUser(user);

            if (res == null)
            {
                return Problem("Entity set 'WebApplication1Context.User'  is null.");
            } else if (res == false)
            {
                return Conflict();
            }

            return CreatedAtAction("GetUser", new { id = user.Name }, user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(User user)
        {
            if (user.Password == null)
            {
                return ValidationProblem("password field is requierd.");
            }
            bool? res = await _service.ValidateUser(user);
            if (res == null)
            {
                return Problem("Entity set 'WebApplication1Context.User'  is null.");

            } else if (res == false)
            {
                return NotFound();

            }

            Signin(user);

            return Ok();
        }
    

        private async void Signin(User account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Id)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Expires
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}