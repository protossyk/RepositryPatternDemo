using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    public abstract class BaseEventBus : DisposableObject,IEventBus
    {
        private readonly IEventExecutContext _eventSubscriptionManger;
        protected BaseEventBus(IEventExecutContext eventSubscriptionManger)
        {
            _eventSubscriptionManger = eventSubscriptionManger;
        }
        public bool IsSubscribed<T, TH>()
        {
           return  _eventSubscriptionManger.HasSubscriptions<T, TH>();
        }

        public abstract Task Publish(IEventData @event);

        public virtual void Subscribe<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>
        {
            _eventSubscriptionManger.AddSubscription<T, TH>();
        }

        public virtual void Unsubscribe<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>
        {
            _eventSubscriptionManger.RemoveSubscription<T, TH>();
        }
    }
}
