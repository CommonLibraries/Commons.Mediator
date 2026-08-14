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
        Func<TRequest, RequestMiddlewareContext, Task> next = async (req, ctx) =>
        {
            await handler.Handle(req, ctx.CancellationToken);
        };
        var context = new RequestMiddlewareContext()
        {
            CancellationToken = cancellationToken,
            ContextKey = this.contextLookup.Get(handler.GetType())
        };

        var middlewares = this.serviceProvider.GetServices<IRequestMiddleware>();
        foreach (var middleware in middlewares.Reverse())
        {
            var currentNext = next;
            next = async (req, context) =>
            {
                await middleware.Invoke(req, context, currentNext);
            };
        }

        await next(request, context);
    }

    public virtual async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var handler = this.serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var context = new RequestMiddlewareContext()
        {
            CancellationToken = cancellationToken,
            ContextKey = this.contextLookup.Get(handler.GetType())
        };

        Func<TRequest, RequestMiddlewareContext, Task<TResponse>> next = async (req, ctx) =>
        {
            return await handler.Handle(req, ctx.CancellationToken);
        };

        var middlewares = this.serviceProvider.GetServices<IRequestMiddleware>();
        foreach (var middleware in middlewares.Reverse())
        {
            var currentNext = next;
            next = async (req, ctx) =>
            {
               return await middleware.Invoke(req, ctx, currentNext);
            };
        }

        var result = await next(request, context);
        return result;
    }
}
