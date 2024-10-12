using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Dtos.Account;
using webapi.Initerfaces;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _itokenservice;
        private readonly SignInManager<AppUser> _siginManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService itokenservice, SignInManager<AppUser> siginManager)
        {
            _userManager = userManager;
            _itokenservice = itokenservice;
            _siginManager = siginManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = appUser.UserName!,
                                Email = appUser.Email!,
                                Token = await _itokenservice.CreateToken(appUser)

                            });

                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LogDto logDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logDto.Username);
            if (user == null) return Unauthorized("The account does not exist");
            var result = await _siginManager.CheckPasswordSignInAsync(user, logDto.Password!, false);
            if (!result.Succeeded) return Unauthorized("Wrong password");
            return Ok(new NewUserDto
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = await _itokenservice.CreateToken(user)
            });

            /* var roles = await _userManager.GetRolesAsync(user);

                    // Chuyển đổi danh sách roles thành danh sách tên role
                    var roleNames = roles.ToList();

                    return Ok(roleNames);*/



        }
    }
}