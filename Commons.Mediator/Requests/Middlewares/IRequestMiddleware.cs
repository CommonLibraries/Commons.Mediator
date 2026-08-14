using Commons.Mediator.Requests;

namespace Commons.Mediator.Requests.Middlewares;

public interface IRequestMiddleware
{
    Task Invoke<TRequest>(
        TRequest request,
        RequestMiddlewareContext context,
        Func<TRequest, RequestMiddlewareContext, Task> next)
            where TRequest : IRequest;

    Task<TResponse> Invoke<TRequest, TResponse>(
        TRequest request,
        RequestMiddlewareContext context,
        Func<TRequest, RequestMiddlewareContext, Task<TResponse>> next)
            where TRequest : IRequest<TResponse>;
}
