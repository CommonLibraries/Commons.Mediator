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
            services.TryAddSingleton<IRequestDispatcher, RequestDispatcher>();
            services.TryAddSingleton<INotificationDispatcher, NotificationDispatcher>();
            services.TryAddSingleton<IMediator, Mediator>();
            return new DefaultMediatorServiceBuilder(services);
        }
    }
}
