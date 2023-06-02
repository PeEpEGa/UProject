using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TODOList.Domain.Exceptions;
using TODOListUniProject.Contracts.Http;
using TODOListUniProject.Domain.Commands;
using TODOListUniProject.UnitTests.Base;

namespace TODOList.UnitTests.Commands;

public class CreateObjectiveCommandTest : BaseHandlerTest<CreateObjectiveCommand, CreateObjectiveCommandResult>
{
    public CreateObjectiveCommandTest() : base()
    {
    }

    protected override IRequestHandler<CreateObjectiveCommand, CreateObjectiveCommandResult> CreateHandler()
    {
        return new CreateObjectiveCommandHandler(DbContext, new Mock<ILogger<CreateObjectiveCommandHandler>>().Object);
    }

    [Fact]
    public async Task HandleShouldCreateObjective()
    {
        //Arrange
        var title = new Guid().ToString();
        var command = new  CreateObjectiveCommand
        {
            Title = title
        };

        //Act
        var result = await Handler.Handle(command, CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.Objective.Title.ShouldBe(title);
        result.Objective.Id.ShouldBeGreaterThan(0);
        result.Objective.IsCompleted.ShouldBe(false);
    }
}