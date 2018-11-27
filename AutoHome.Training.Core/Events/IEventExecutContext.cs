using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    /// <summary>
    /// 1.事件注册
    /// 2.判断事件是否已注册
    /// 3.事件处理方法Handle
    /// </summary>
    public interface IEventExecutContext
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
           where T : IEventData
           where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IEventHandler<T>
             where T : IEventData;

        bool HasSubscriptionsForEvent<T>() where T : IEventData;
        bool HasSubscriptionsForEvent(string eventName);
        bool HasSubscriptions<T, TH>();
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<Type> GetHandlersForEvent<T>() where T : IEventData;
        IEnumerable<Type> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
        Task HandleAsync(IEventData @event, CancellationToken cancellationToken = default);
    }
}
