using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Clays.App.Domain;
using Clays.App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clay.App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginservice;
        public AccountController(ILoginService loginService)
        {
            _loginservice = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _loginservice.UserLogin(loginModel);
            if (HttpStatusCode.OK == (HttpStatusCode)result.StatusCode)
            {
                return Ok(result.Result);
            }
            else
            {
                return Unauthorized(result.ErrorMessage);
            }
        }
    }
}

