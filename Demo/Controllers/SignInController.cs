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
    [Route("api/signin")]
    [ApiController]
   // [Authorize]
    public class SignInController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public SignInController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpPost("{userName}")]
        public async Task<IActionResult> Login(string userName, UserAdd UserAdd)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();             
            }
            return Ok();
        }
    }
}
