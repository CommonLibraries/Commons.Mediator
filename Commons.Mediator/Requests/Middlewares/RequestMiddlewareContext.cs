namespace Commons.Mediator.Requests.Middlewares;

public class RequestMiddlewareContext
{
    public required string? ContextKey { get; init; }
    public required CancellationToken CancellationToken { get; init; }
}
