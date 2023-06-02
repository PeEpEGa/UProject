using MediatR;
using Microsoft.Extensions.Logging;

namespace TODOListUniProject.Domain.Base;

internal abstract class BaseHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    protected readonly ILogger Logger;
    private readonly string _name;

    protected BaseHandler(ILogger logger)
    {
        Logger = logger;
        _name = GetType().Name;
    }
    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogDebug("Start to execute {Type}. Input: {@Input}", _name, request);
            
            var result = await HandleInternal(request, cancellationToken);

            Logger.LogDebug("Executed to execute {Type}. Result: {@Input}", _name, result);

            return result;
        }
        catch (Exception)
        {
            Logger.LogError("Exception raised. Input {@Input}", request);
            throw;
        }
    }

    protected abstract Task<TResult> HandleInternal(TRequest request, CancellationToken cancellationToken);
}