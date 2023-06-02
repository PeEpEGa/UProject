using MediatR;
using TODOListUniProject.Domain.Database;
using TODOListUniProject.UnitTests.Helpers;

namespace TODOListUniProject.UnitTests.Base;

public abstract class BaseHandlerTest<TRequest, TResult> : IDisposable
    where TRequest : IRequest<TResult>
{
    protected readonly ListDbContext DbContext;
    protected readonly IRequestHandler<TRequest, TResult> Handler;

    public BaseHandlerTest()
    {
        DbContext = DbContextHelper.CreateTestDb();
        Handler = CreateHandler();
    }

    protected abstract IRequestHandler<TRequest, TResult> CreateHandler();

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}