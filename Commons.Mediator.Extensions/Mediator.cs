using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IRequestDispatcher requestDispatcher;

        private readonly INotificationDispatcher notificationDispatcher;

        public Mediator(IRequestDispatcher requestDispatcher, INotificationDispatcher notificationDispatcher)
        {
            this.requestDispatcher = requestDispatcher;
            this.notificationDispatcher = notificationDispatcher;
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return this.requestDispatcher.Send(request, cancellationToken);
        }

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
        {
            return this.requestDispatcher.Send<TRequest, TResponse>(request, cancellationToken);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return this.notificationDispatcher.Publish(notification, cancellationToken);
        }
    }
}
