using MediatR;
using Microsoft.Extensions.Logging;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Base;
using TODOListUniProject.Domain.Database;

namespace TODOListUniProject.Domain.Commands;

public class UpdateObjectiveCommand : IRequest<UpdateObjectiveCommandResult>
{
    public int ObjectiveId { get; set; }
    public string Title { get; set; }
}

public class UpdateObjectiveCommandResult
{
    public int ObjectiveId { get; set; }
}



internal class UpdateObjectiveCommandHandler : BaseHandler<UpdateObjectiveCommand, UpdateObjectiveCommandResult>
{
    private readonly ListDbContext _dbContext;

    public UpdateObjectiveCommandHandler(ListDbContext dbContext, ILogger<UpdateObjectiveCommandHandler> logger) : base(logger)
    {
        _dbContext = dbContext;
    }
    protected override async Task<UpdateObjectiveCommandResult> HandleInternal(UpdateObjectiveCommand request, CancellationToken cancellationToken)
    {
        var objectiveId = request.ObjectiveId;

        var objectiveToUpdate = _dbContext.Objectives.SingleOrDefault(o => o.Id == objectiveId);
        if(objectiveToUpdate == null)
        {
            throw new ListException(ErrorCode.ObjectiveNotFound, $"Objective {objectiveId} not found");
        }

        objectiveToUpdate.Title = request.Title;
        await _dbContext.SaveChangesAsync();

        return new UpdateObjectiveCommandResult
        {
            ObjectiveId = objectiveId
        };
    }
}