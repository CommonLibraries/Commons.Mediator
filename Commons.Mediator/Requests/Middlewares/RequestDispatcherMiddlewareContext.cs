namespace Commons.Mediator.Requests.Middlewares;

public class RequestDispatcherMiddlewareContext
{
    public required string? ContextKey { get; init; }
    public required CancellationToken CancellationToken { get; init; }
}
