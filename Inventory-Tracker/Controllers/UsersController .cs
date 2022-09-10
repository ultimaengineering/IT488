using Microsoft.AspNetCore.Mvc;

namespace Inventory_Tracker.Controllers;
using Inventory_Tracker.Helpers;
using Inventory_Tracker.Models;
using Inventory_Tracker.Services;

[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        if (response == null)
        {
            return Unauthorized(new { message = "Username or password is incorrect" });
        }
        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreationRequest model)
    {
        var response = _userService.CreateUser(model);
        if (response == default)
        {
            return BadRequest("Username unavalible. Please pick another username.");
        }

        return Ok(response);
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser([FromBody] UpdateUserRequest model)
    {
        var response = _userService.UpdateUser(model);
        if (response == null)
        {
            return BadRequest("Unable to process your request at this time.");
        }

        return Ok(response);
    }


    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}
