using Commons.Mediator.Requests;
using Commons.Mediator.Requests.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Mediator.Extensions.Requests;

public class DefaultRequestDispatcher : IRequestDispatcher
{
    private readonly IServiceProvider serviceProvider;
    private readonly IRequestHandlerContextLookup contextLookup;

    public DefaultRequestDispatcher(IServiceProvider serviceProvider, IRequestHandlerContextLookup contextLookup)
    {
        this.serviceProvider = serviceProvider;
        this.contextLookup = contextLookup;
    }

    public virtual async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var handler = this.serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();

        Func<TRequest, Task> next = async (req) =>
        {
            await handler.Handle(request, cancellationToken);
        };
        var context = new RequestDispatcherMiddlewareContext()
        {
            CancellationToken = cancellationToken,
            ContextKey = this.contextLookup.Get(handler.GetType())
        };

        var middlewares = this.serviceProvider.GetServices<IRequestMiddleware>();
        foreach (var middleware in middlewares)
        {
            var currentNext = next;
            next = async (req) =>
            {
                await middleware.Invoke(req, context, currentNext);
            };
        }

        await next(request);
    }

    public virtual async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var handler = this.serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var context = new RequestDispatcherMiddlewareContext()
        {
            CancellationToken = cancellationToken,
            ContextKey = this.contextLookup.Get(handler.GetType())
        };

        var middlewares = this.serviceProvider.GetServices<IRequestMiddleware>();
        Func<TRequest, Task<TResponse>> next = async (req) =>
        {
            return await handler.Handle(request, cancellationToken);
        };
        foreach (var middleware in middlewares)
        {
            var currentNext = next;
            next = async (req) =>
            {
               return await middleware.Invoke<TRequest, TResponse>(req, context, currentNext);
            };
        }

        var result = await next(request);
        return result;
    }
}
