using MediatR;
using Microsoft.Extensions.Logging;
using TODOListUniProject.Contracts.Database;
using TODOListUniProject.Domain.Base;
using TODOListUniProject.Domain.Database;

namespace TODOListUniProject.Domain.Commands;

public class CreateObjectiveCommand : IRequest<CreateObjectiveCommandResult>
{
    public string Title { get; init; }
}

public class CreateObjectiveCommandResult
{
    public Objective Objective;
}

internal class CreateObjectiveCommandHandler : BaseHandler<CreateObjectiveCommand, CreateObjectiveCommandResult>
{
    private readonly ListDbContext _dbContext;

    public CreateObjectiveCommandHandler(ListDbContext listDbContext, ILogger<CreateObjectiveCommandHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<CreateObjectiveCommandResult> HandleInternal(CreateObjectiveCommand request, CancellationToken cancellationToken)
    {
        var objective = new Objective
        {
            Title = request.Title
        };

        await _dbContext.AddAsync(objective, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateObjectiveCommandResult
        {
            Objective = objective
        };
    }
}