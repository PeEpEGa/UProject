using MediatR;
using Microsoft.AspNetCore.Mvc;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Api.Controllers;
using TODOListUniProject.Domain.Commands;
using TODOListUniProject.Domain.Queries;

namespace TODOList.Api.Controllers;

[Route("api/objective")]
public class ObjectiveController : BaseController
{
    private readonly IMediator _mediator;

    public ObjectiveController(IMediator mediator, ILogger<ObjectiveController> logger) : base(logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<IActionResult> CreateObjective([FromBody]CreateObjectiveRequest request, CancellationToken cancellationToken)
    {
        return SafeExecute(async () => 
        {
            if(!ModelState.IsValid)
            {
                return ToActionResult(new ErrorResponse {
                    Code = ErrorCode.BadRequest,
                    Message = "Invalid request"
                });
            }

            var command = new CreateObjectiveCommand
            {
                Title = request.Title
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new CreateObjectiveResponse
            {
                ObjectiveId = result.Objective.Id
            };

            return Created($"http://{Request.Host}/api/objective/{response.ObjectiveId}", response);
        }, cancellationToken);
    }


    [HttpDelete]
    public Task<IActionResult> DeleteObjective(int objectiveId, CancellationToken cancellationToken)
    {
        return SafeExecute(async () =>
        {
            var command = new DeleteObjectiveCommand
            {
                ObjectiveId = objectiveId
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new ObjectiveResponse
            {
                Message = $"Objective with id: {objectiveId} was deleted."
            };

            return Ok(response);
        }, cancellationToken);
    }


    [HttpPut]
    public Task<IActionResult> UpdateObjective(int objectiveId, string title, CancellationToken cancellationToken)
    {
        return SafeExecute(async () =>
        {
            var command = new UpdateObjectiveCommand
            {
                ObjectiveId = objectiveId,
                Title = title
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new ObjectiveResponse
            {
                Message = $"Objective with id: {objectiveId} was updated."
            };

            return Ok(response);
        }, cancellationToken);
    }


    [HttpGet]
    [ProducesResponseType(typeof(ObjectivesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetObjectives(CancellationToken cancellationToken)
    {
        return SafeExecute(async () =>
        {
            var query = new GetAllObjectivesQuery{};

            var result = await _mediator.Send(query, cancellationToken);

            var response = new ObjectivesResponse
            {
                Objectives = result.Objectives
            };

            return Ok(response.Objectives);
        }, cancellationToken);
    }
}