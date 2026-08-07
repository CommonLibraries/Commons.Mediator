using Commons.Mediator.Extensions.Notifications;
using Commons.Mediator.Extensions.Notifications.NotificationPublishMethods;
using Commons.Mediator.Extensions.Requests;
using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using Commons.Mediator.Requests.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.Mediator.Extensions;

public static class MediatorServiceCollectionExtensions
{
    public static IMediatorServiceBuilder AddMediator(this IServiceCollection services)
    {
        var requestHandlerLookup = new Dictionary<Type, string>();
        services.TryAddTransient<IRequestHandlerContextLookup>((sp) => new RequestHandlerContextLookup(requestHandlerLookup));
        services.TryAddTransient<IRequestDispatcher, DefaultRequestDispatcher>();
        services.TryAddTransient<INotificationDispatcher, NotificationDispatcher>();
        services.TryAddTransient<IMediator, Mediator>();
        services.TryAddTransient<INotificationPublishMethod, SequentialNotificationPublishMethod>();
        return new DefaultMediatorServiceBuilder(services, requestHandlerLookup);
    }
}
