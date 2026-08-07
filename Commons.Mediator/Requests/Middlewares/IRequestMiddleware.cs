using Commons.Mediator.Requests;

namespace Commons.Mediator.Requests.Middlewares;

public interface IRequestMiddleware
{
    Task Invoke<TRequest>(TRequest request, RequestDispatcherMiddlewareContext context, Func<TRequest, Task> next)
        where TRequest : IRequest;

    Task<TResponse> Invoke<TRequest, TResponse>(TRequest request, RequestDispatcherMiddlewareContext context, Func<TRequest, Task<TResponse>> next)
        where TRequest : IRequest<TResponse>;
}
