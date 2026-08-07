using Commons.Mediator.Extensions.Notifications.NotificationPublishMethods;
using Commons.Mediator.Extensions.Requests.Middlewares;
using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using Commons.Mediator.Requests.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Commons.Mediator.Extensions;

internal class DefaultMediatorServiceBuilder : IMediatorServiceBuilder
{
    private readonly IServiceCollection serviceCollections;
    private readonly IDictionary<Type, string>? requestHandlerContextLookup;

    public DefaultMediatorServiceBuilder(IServiceCollection serviceCollections,
        IDictionary<Type, string>? requestHandlerContextLookup)
    {
        this.serviceCollections = serviceCollections;
        this.requestHandlerContextLookup = requestHandlerContextLookup;
    }

    public IMediatorServiceBuilder AddNotificationHandlers(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsClass) continue;
            if (type.IsAbstract) continue;

            Type? typeInterface;
            typeInterface = type.GetInterface(typeof(INotificationHandler<>).Name);
            if (typeInterface is null) continue;

            this.serviceCollections.TryAddTransient(typeInterface, type);
        }

        return this;
    }

    public IMediatorServiceBuilder AddRequestHandlers(Assembly assembly, string? contextKey = null)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsClass) continue;
            if (type.IsAbstract) continue;

            Type? typeInterface;
            typeInterface = type.GetInterface(typeof(IRequestHandler<>).Name) ??
                            type.GetInterface(typeof(IRequestHandler<,>).Name);
            if (typeInterface is null) continue;

            this.serviceCollections.TryAddTransient(typeInterface, type);
            if (contextKey is not null)
            {
                this.requestHandlerContextLookup?[type] = contextKey;
            }
        }

        return this;
    }

    public IMediatorServiceBuilder AddRequestMiddleware<TMiddleware>() where TMiddleware : class, IRequestMiddleware
    {
        this.serviceCollections.TryAddTransient<IRequestMiddleware, TMiddleware>();
        return this;
    }

    public IMediatorServiceBuilder UseLogRequestMiddleware()
    {
        this.AddRequestMiddleware<LogRequestMiddleware>();
        return this;
    }

    public IMediatorServiceBuilder UseParallelNotificationPublishMethod()
    {
        this.serviceCollections.RemoveAll<INotificationPublishMethod>();
        this.serviceCollections.TryAddSingleton<INotificationPublishMethod, ParallelNotificationPublishMethod>();
        return this;
    }

    public IMediatorServiceBuilder UseSequentialNotificationPublishMethod()
    {
        this.serviceCollections.RemoveAll<INotificationPublishMethod>();
        this.serviceCollections.TryAddSingleton<INotificationPublishMethod, SequentialNotificationPublishMethod>();
        return this;
    }
}
