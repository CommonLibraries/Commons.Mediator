namespace Commons.Mediator.Notifications
{
    public interface INotificationDispatcher
    {
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification;
    }
}
