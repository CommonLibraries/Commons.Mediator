using Commons.Mediator.Requests;
using Commons.Mediator.Requests.Middlewares;
using Microsoft.Extensions.Logging;

namespace Commons.Mediator.Extensions.Requests.Middlewares;

public class LogRequestMiddleware : IRequestMiddleware
{
    private readonly ILogger<LogRequestMiddleware> logger;
    public LogRequestMiddleware(ILogger<LogRequestMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task Invoke<TRequest>(
        TRequest request,
        RequestMiddlewareContext context,
        Func<TRequest, RequestMiddlewareContext, Task> next) where TRequest : IRequest
    {
        try
        {
            await next(request, context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<TResponse> Invoke<TRequest, TResponse>(
        TRequest request,
        RequestMiddlewareContext context,
        Func<TRequest, RequestMiddlewareContext, Task<TResponse>> next) where TRequest : IRequest<TResponse>
    {
        try
        {
            return await next(request, context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
