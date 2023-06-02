using MediatR;
using Microsoft.Extensions.Logging;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Base;
using TODOListUniProject.Domain.Database;

namespace TODOListUniProject.Domain.Commands;

public class DeleteObjectiveCommand : IRequest<DeleteObjectiveCommandResult>
{
    public int ObjectiveId { get; set; }
}

public class DeleteObjectiveCommandResult
{
    public int ObjectiveId { get; set; }
}



internal class DeleteObjectiveCommandHandler : BaseHandler<DeleteObjectiveCommand, DeleteObjectiveCommandResult>
{
    private readonly ListDbContext _dbContext;

    public DeleteObjectiveCommandHandler(ListDbContext dbContext, ILogger<DeleteObjectiveCommandHandler> logger) : base(logger)
    {
        _dbContext = dbContext;
    }
    protected override async Task<DeleteObjectiveCommandResult> HandleInternal(DeleteObjectiveCommand request, CancellationToken cancellationToken)
    {
        var objectiveId = request.ObjectiveId;

        var objectiveToDelete = _dbContext.Objectives.SingleOrDefault(o => o.Id == objectiveId);
        if(objectiveToDelete == null)
        {
            throw new ListException(ErrorCode.ObjectiveNotFound, $"Objective {objectiveId} not found");
        }

        _dbContext.Remove(objectiveToDelete);
        await _dbContext.SaveChangesAsync();

        return new DeleteObjectiveCommandResult
        {
            ObjectiveId = objectiveId
        };
    }
}