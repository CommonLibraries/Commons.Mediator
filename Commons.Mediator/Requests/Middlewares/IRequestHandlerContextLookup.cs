namespace Commons.Mediator.Requests.Middlewares;

public interface IRequestHandlerContextLookup
{
    string? Get(Type handlerType);
}
