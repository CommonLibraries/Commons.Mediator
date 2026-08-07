using Commons.Mediator.Requests.Middlewares;

namespace Commons.Mediator.Extensions.Requests;

public class RequestHandlerContextLookup : IRequestHandlerContextLookup
{
    private readonly IDictionary<Type, string> contexts;
    public RequestHandlerContextLookup(IDictionary<Type, string> contexts)
    {
        this.contexts = contexts;
    }

    public string? Get(Type handlerType)
    {
        this.contexts.TryGetValue(handlerType, out var context);
        return context;
    }
}
