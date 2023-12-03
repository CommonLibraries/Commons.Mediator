using Commons.Mediator.NotificationPublishMethods;
using Commons.Mediator.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Mediator
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        private readonly INotificationPublishMethod publishMethod;

        public NotificationDispatcher(IServiceProvider serviceProvider, INotificationPublishMethod publishMethod)
        {
            this.serviceProvider = serviceProvider;
            this.publishMethod = publishMethod;
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            var handlers = this.serviceProvider.GetServices<INotificationHandler<TNotification>>();
            await this.publishMethod.Execute(handlers, notification, cancellationToken);
        }
    }
}
