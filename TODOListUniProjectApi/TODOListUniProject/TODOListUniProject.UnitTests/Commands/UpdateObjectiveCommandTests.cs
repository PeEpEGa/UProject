using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Database;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Commands;
using TODOListUniProject.UnitTests.Base;

namespace TODOList.UnitTests.Commands;

public class UpdateObjectiveCommandTests : BaseHandlerTest<UpdateObjectiveCommand, UpdateObjectiveCommandResult>
{
    public UpdateObjectiveCommandTests() : base()
    {
    }

    protected override IRequestHandler<UpdateObjectiveCommand, UpdateObjectiveCommandResult> CreateHandler()
    {
        return new UpdateObjectiveCommandHandler(DbContext, new Mock<ILogger<UpdateObjectiveCommandHandler>>().Object);
    }

    [Fact]
    public async Task HandleShouldUpdateObjective()
    {
        //Arrange
        var id = 1;
        var title = "a";

        var list = new List<Objective>
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
        DbContext.AddRange(list);
        DbContext.SaveChanges();

        var command = new  UpdateObjectiveCommand
        {
            ObjectiveId = id,
            Title = title
        };

        //Act
        var result = await Handler.Handle(command, CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.ObjectiveId.ShouldBe(id);
    }

    [Fact]
    public async Task HandleShouldThrowException()
    {
        //Arrange
        var id = -1;
        var title = "a";

        var command = new  UpdateObjectiveCommand
        {
            ObjectiveId = id,
            Title = title
        };

        try
        {
            //Act
            await Handler.Handle(command, CancellationToken.None);
        }
        catch (ListException le) when (le.ErrorCode == ErrorCode.ObjectiveNotFound && le.Message == $"Objective {id} not found")
        {
            //Assert
        }
    }
}