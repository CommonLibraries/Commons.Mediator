using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Mediator.Extensions
{
    public interface IMediatorServiceBuilder
    {
        IMediatorServiceBuilder AddRequestHandlers(Assembly assembly);
        IMediatorServiceBuilder AddNotificationHandlers(Assembly assembly);
    }
}
