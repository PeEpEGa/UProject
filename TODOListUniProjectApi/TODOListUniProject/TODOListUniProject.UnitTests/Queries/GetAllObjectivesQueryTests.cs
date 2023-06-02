using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Database;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Commands;
using TODOListUniProject.Domain.Queries;
using TODOListUniProject.UnitTests.Base;
using TODOListUniProject.UnitTests.Helpers;

namespace TODOList.UnitTests.Commands;

public class GetAllObjectivesQueryTests : BaseHandlerTest<GetAllObjectivesQuery, GetAllObjectivesResult>
{
    public GetAllObjectivesQueryTests() : base()
    {
    }

    protected override IRequestHandler<GetAllObjectivesQuery, GetAllObjectivesResult> CreateHandler()
    {
        return new GetAllObjectivesQueryHandler(DbContext, new Mock<ILogger<GetAllObjectivesQueryHandler>>().Object);
    }

    [Fact]
    public async Task HandleShouldReturnAllObjectives()
    {
        //Arrange
        var dbContext = DbContextHelper.CreateTestDb(DbContext.Database.GetDbConnection().ConnectionString);

        var objectives = new List<Objective>
        {
            new Objective
            {
                Id = 1,
                Title = Guid.NewGuid().ToString(),
                IsCompleted = true
            },
            new Objective
            {
                Id = 2,
                Title = Guid.NewGuid().ToString(),
                IsCompleted = false
            },
            new Objective
            {
                Id = 3,
                Title = Guid.NewGuid().ToString(),
                IsCompleted = true
            }
        };

        await dbContext.AddRangeAsync(objectives);
        await dbContext.SaveChangesAsync();


        var query = new GetAllObjectivesQuery
        {
            ListId = 1
        };

        //Act
        var result = await Handler.Handle(query, CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.Objectives.ShouldNotBeNull();
        foreach (var objective in result.Objectives)
        {
            objective.Title.ShouldNotBeNull();
            objective.Id.ShouldBeGreaterThan(0);
        }
    }
}