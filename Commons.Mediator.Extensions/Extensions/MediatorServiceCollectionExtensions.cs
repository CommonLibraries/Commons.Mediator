using Commons.Mediator.NotificationPublishMethods;
using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.Mediator.Extensions
{
    public static class MediatorServiceCollectionExtensions
    {
        public static IMediatorServiceBuilder AddMediator(this IServiceCollection services)
        {
            services.TryAddTransient<IRequestDispatcher, RequestDispatcher>();
            services.TryAddTransient<INotificationDispatcher, NotificationDispatcher>();
            services.TryAddTransient<IMediator, Mediator>();
            services.TryAddTransient<INotificationPublishMethod, SequentialNotificationPublishMethod>();
            return new DefaultMediatorServiceBuilder(services);
        }
    }
}
