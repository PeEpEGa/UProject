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

public class DeleteObjectiveCommandTests : BaseHandlerTest<DeleteObjectiveCommand, DeleteObjectiveCommandResult>
{
    public DeleteObjectiveCommandTests() : base()
    {
    }

    protected override IRequestHandler<DeleteObjectiveCommand, DeleteObjectiveCommandResult> CreateHandler()
    {
        return new DeleteObjectiveCommandHandler(DbContext, new Mock<ILogger<DeleteObjectiveCommandHandler>>().Object);
    }

    [Fact]
    public async Task HandleShouldDeleteObjective()
    {
        //Arrange
        var id = 1;

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

        var command = new  DeleteObjectiveCommand
        {
            ObjectiveId = id
        };

        //Act
        var result = await Handler.Handle(command, CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.ObjectiveId.ShouldBe(id);
        DbContext.Objectives.Count().ShouldBe(list.Count - 1);
    }

    [Fact]
    public async Task HandleShouldThrowException()
    {
        //Arrange
        var id = -1;

        var command = new  DeleteObjectiveCommand
        {
            ObjectiveId = id
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