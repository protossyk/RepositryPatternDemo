using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    public class EventExecuteContext : IEventExecutContext
    {
        //handler是在程序启动时候加载，不存在并发，不需要concurrent
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;
        private readonly IServiceProvider _iServiceProvider;

        public EventExecuteContext(IServiceProvider serviceProvider)
        {
            _iServiceProvider = serviceProvider;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();


        public void AddSubscription<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            DoAddSubscription(typeof(TH), eventName, isDynamic: false);
            _eventTypes.Add(typeof(T));
        }

        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);
        }



        public void RemoveSubscription<T, TH>()
            where TH : IEventHandler<T>
            where T : IEventData
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            DoRemoveHandler(eventName, handlerToRemove);
        }


        private void DoRemoveHandler(string eventName, Type subsToRemove)
        {
            if (subsToRemove != null)
            {
                _handlers[eventName].Remove(subsToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }

            }
        }

        public IEnumerable<Type> GetHandlersForEvent<T>() where T : IEventData
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }
        public IEnumerable<Type> GetHandlersForEvent(string eventName) => _handlers[eventName];

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            if (handler != null)
            {
                OnEventRemoved(this, eventName);
            }
        }




        private Type FindSubscriptionToRemove<T, TH>()
             where T : IEventData
             where TH : IEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }

        private Type DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s == handlerType);

        }

        public bool HasSubscriptionsForEvent<T>() where T : IEventData
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);
        public bool HasSubscriptions<T, TH>()
        {
            var eventName = GetEventKey<T>();
            return _handlers[eventName].Any(s => s == typeof(TH));
        }

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }


        public async Task HandleAsync(IEventData @event, CancellationToken cancellationToken = default)
        {
            var eventName = @event.GetType().Name;
            if (HasSubscriptionsForEvent(eventName))
            {
                using (var serviceScope = _iServiceProvider.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var subscriptions = GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = (IEventHandler)serviceScope.ServiceProvider.GetService(subscription);
                        if (handler.CanHandle(@event))
                        {
                            await handler.HandleAsync(@event, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}
