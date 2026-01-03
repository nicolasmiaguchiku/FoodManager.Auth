using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Application.Input.Commands;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FoodManger.Auth.WebApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(ICommandMediator commandMediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest Request, CancellationToken cancellationToken)
    {
        var result = await commandMediator.SendAsync(new LoginCommand(Request), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Error);
    }
}