using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Clays.app.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Clays.App.Domain.Interfaces;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clay.App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoorController : ControllerBase
    {
        public IHttpContextAccessor _httpContextAccessor;
        public IDoorService _doorService;
        public DoorController(IHttpContextAccessor httpContextAccessor,IDoorService doorService)
        {
            _httpContextAccessor = httpContextAccessor;
            _doorService = doorService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("AddDoor")]
        public async Task<IActionResult> AddDoor(Door door)
        {
            var result = await _doorService.AddDoor(door);
            if (result.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize]
        [HttpGet("OpenDoor")]
        public async Task<IActionResult> OpenDoor(int doorId)
        {
            var open = await _doorService.OpenDoor(doorId);
            if (open.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(open.Result);
            }
            else
            {
                return BadRequest(open);
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("DoorHistory")]
        public async Task<IActionResult> DoorHistory()
        {
            var history = await _doorService.GetDoorHistory();
            if(history.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(history.Result);
            }
            else
            {
                return BadRequest(history.ErrorMessage);
            }
        }
    }
}

