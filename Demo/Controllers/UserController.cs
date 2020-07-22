using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Demo.Models;
using Demo.DtoParameters;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }
        [HttpGet(Name =nameof(UserManagement))]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> UserManagement()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            return NoContent();

        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> AddUser(UserAdd UserAdd)
        {
            var exist = await _userManager.FindByNameAsync(UserAdd.UserName);
            if (exist != null)
            {
                return BadRequest();
            }
            var user = new ApplicationUser
            {
                UserName = UserAdd.UserName,
                Email=UserAdd.Email,
                Name=UserAdd.Name,
                BirthDate=UserAdd.BirthDate
            };
            await _userManager.CreateAsync(user, UserAdd.Password);
            var dtoToRetrun = _mapper.Map<ApplicationUser>(UserAdd);
            return CreatedAtRoute(nameof(UserManagement), null, dtoToRetrun);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> EditUser(string userId,UserAdd userAdd)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.PasswordHash = userAdd.Password;
            user.Email = userAdd.Email;
            user.BirthDate = userAdd.BirthDate;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }
        [HttpPost("{userId}")]
        public async Task<ActionResult> ChangePassword(string userId, ChangePasswordModel changePasswordModel)
        {
            var userModel =await _userManager.FindByNameAsync(userId);
            if (userModel == null)
            {
                return NotFound();
            }
            await _userManager.ChangePasswordAsync(userModel, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
            return NoContent();
        }

    }
}
