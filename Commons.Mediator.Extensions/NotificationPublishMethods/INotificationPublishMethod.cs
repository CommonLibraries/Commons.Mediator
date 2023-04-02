using Commons.Mediator.Notifications;

namespace Commons.Mediator.NotificationPublishMethods
{
    public interface INotificationPublishMethod
    {
        Task Execute<TNotification>(
            IEnumerable<INotificationHandler<TNotification>> handlers,
            TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;
    }
}
