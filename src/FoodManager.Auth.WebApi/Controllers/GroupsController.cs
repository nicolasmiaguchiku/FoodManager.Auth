using FoodManager.Auth.Application.Input.Commands;
using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Application.Output.Queries;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FoodManger.Auth.WebApi.Controllers
{
    [Route("api/v1/groups")]
    [ApiController]
    public class GroupsController(ICommandMediator commandMediator, IQueryMediator queryMediator) : ControllerBase
    {
        /// <summary>
        /// Adds a new group to the specified Keycloak realm.
        /// </summary>
        /// <returns>
        /// A 201 Created status code along with a success message if the group is successfully created;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroup([FromBody] AddGroupRequest addGroupRequest, CancellationToken cancellationToken)
        {
            var result = await commandMediator.SendAsync(new AddGroupCommand(addGroupRequest), cancellationToken);

            if (result.IsSuccess)
            {
                var response = Result<string>.Success("Group created successfully");
                return Created("/createGroup", response);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Deletes an existing group from the specified Keycloak realm.
        /// </summary>
        /// <returns>
        /// A 204 No Content status code if the group was successfully deleted;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await commandMediator.SendAsync(new DeleteGroupCommand(id), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Returns all groups registered in the realm
        /// </summary>
        /// <returns>
        /// A 200 OK status code along with the list of groups if the operation is successful;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetGroupByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await queryMediator.QueryAsync(new GetGroupByIdQuery(id), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Returns all groups registered in the realm
        /// </summary>
        /// <returns>
        /// A 200 OK status code along with the list of groups if the operation is successful;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllGroupsAsync(CancellationToken cancellationToken)
        {
            var result = await queryMediator.QueryAsync(new GetAllGroupsQuery(), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Error);
        }
    }
}
