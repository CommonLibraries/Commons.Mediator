using Commons.Mediator.NotificationPublishMethods;
using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Commons.Mediator.Extensions
{
    internal class DefaultMediatorServiceBuilder : IMediatorServiceBuilder
    {
        private readonly IServiceCollection serviceCollections;
        
        public DefaultMediatorServiceBuilder(IServiceCollection serviceCollections)
        {
            this.serviceCollections = serviceCollections;
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

                this.serviceCollections.TryAddSingleton(typeInterface, type);
            }

            return this;
        }

        public IMediatorServiceBuilder AddRequestHandlers(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass) continue;
                if (type.IsAbstract) continue;

                Type? typeInterface;
                typeInterface = type.GetInterface(typeof(IRequestHandler<>).Name) ??
                                type.GetInterface(typeof(IRequestHandler<,>).Name);
                if (typeInterface is null) continue;

                this.serviceCollections.TryAddSingleton(typeInterface, type);
            }

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
}
