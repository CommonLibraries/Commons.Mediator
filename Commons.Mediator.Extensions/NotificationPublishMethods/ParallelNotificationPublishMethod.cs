using Commons.Mediator.Notifications;

namespace Commons.Mediator.NotificationPublishMethods
{
    public class ParallelNotificationPublishMethod : INotificationPublishMethod
    {
        public async Task Execute<TNotification>(
            IEnumerable<INotificationHandler<TNotification>> handlers,
            TNotification notification,
            CancellationToken cancellationToken = default) where TNotification : INotification
        {
            var tasks = handlers.Select(handler => handler.Handle(notification, cancellationToken)).ToArray();
            await Task.WhenAll(tasks);
        }
    }
}
