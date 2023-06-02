using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Database;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Base;
using TODOListUniProject.Domain.Database;

namespace TODOListUniProject.Domain.Queries;

public class GetAllObjectivesQuery : IRequest<GetAllObjectivesResult>
{
    public int ListId { get; init; }
}

public class GetAllObjectivesResult
{
    public List<Objective> Objectives { get; init; }
}

internal class GetAllObjectivesQueryHandler : BaseHandler<GetAllObjectivesQuery, GetAllObjectivesResult>
{
    private readonly ListDbContext _dbContext;

    public GetAllObjectivesQueryHandler(ListDbContext listDbContext, ILogger<GetAllObjectivesQueryHandler> logger) : base(logger)
    {
        _dbContext = listDbContext;
    }

    protected override async Task<GetAllObjectivesResult> HandleInternal(GetAllObjectivesQuery request, CancellationToken cancellationToken)
    {
        var objectives = await _dbContext.Objectives.ToListAsync();

        return objectives == null ? 
            throw new ListException(ErrorCode.ObjectiveNotFound, "Objectives not found") : 
            new GetAllObjectivesResult
            {
                Objectives = objectives
            };
    }
    
}