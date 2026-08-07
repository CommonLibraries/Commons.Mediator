using Commons.Mediator.Requests.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Mediator.Extensions;

public interface IMediatorServiceBuilder
{
    IMediatorServiceBuilder AddRequestHandlers(Assembly assembly, string? contextKey = null);
    IMediatorServiceBuilder AddRequestMiddleware<TMiddleware>() where TMiddleware : class, IRequestMiddleware;
    IMediatorServiceBuilder AddNotificationHandlers(Assembly assembly);
    IMediatorServiceBuilder UseParallelNotificationPublishMethod();
    IMediatorServiceBuilder UseSequentialNotificationPublishMethod();
    IMediatorServiceBuilder UseLogRequestMiddleware();
}
