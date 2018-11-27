using AutoHome.Training.Core.Events;
using AutoHome.Training.PortalDemo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoHome.Training.PortalDemo.EventHandlers
{
    public class OrderConfirmedEventHandler : IEventHandler<OrderConfirmedEventData>
    {
        public Task HandleAsync(OrderConfirmedEventData @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
