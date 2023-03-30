using Commons.Mediator.Notifications;
using Commons.Mediator.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Mediator
{
    public interface IMediator : IRequestDispatcher, INotificationDispatcher
    {
    }
}
