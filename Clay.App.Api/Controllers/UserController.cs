using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Clays.app.DataAccess.Entities;
using Clays.App.Domain;
using Clays.App.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clay.App.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(RegisterUserModel user)
        {
            var result = await _userService.AddUser(user);
            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok("User Added Successfully");
            else
                return BadRequest(result.ErrorMessage);
        }
    }
}

