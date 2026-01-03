using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Application.Output.Queries;
using LiteBus.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FoodManger.Auth.WebApi.Controllers
{
    [Route("api/v1groups/users")]
    [ApiController]
    public class GroupUsersControllers(IQueryMediator queryMediator) : ControllerBase
    {
        /// <summary>
        /// Retrieves all users present in a specific group within the specified Keycloak realm.
        /// </summary>
        /// <returns>
        /// A 200 OK status code with a list of users in the group;
        /// otherwise, a 400 Bad Request status code with an error message.
        /// </returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsersInGroup([FromQuery] GetUsersGroupRequest usersGroup, CancellationToken cancellationToken)
        {
            var result = await queryMediator.QueryAsync(new GetUsersGroupQuery(usersGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Error);
        }
    }
}
