using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    public abstract class BaseEventHandler<TEventData> : IEventHandler<TEventData>
        where TEventData : IEventData
    {
        public bool CanHandle(IEventData @event)
    => typeof(TEventData).Equals(@event.GetType());

        public abstract Task HandleAsync(TEventData @event, CancellationToken cancellationToken = default);

        public Task HandleAsync(IEventData @event, CancellationToken cancellationToken = default)
            => CanHandle(@event) ? HandleAsync((TEventData)@event, cancellationToken) : Task.FromResult(false);
    }
}
