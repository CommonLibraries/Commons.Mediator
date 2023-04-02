using Commons.Mediator.Notifications;

namespace Commons.Mediator.NotificationPublishMethods
{
    public class SequentialNotificationPublishMethod : INotificationPublishMethod
    {
        public async Task Execute<TNotification>(
            IEnumerable<INotificationHandler<TNotification>> handlers,
            TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            foreach (var handler in handlers)
            {
                await handler.Handle(notification, cancellationToken);
            }
        }
    }
}
