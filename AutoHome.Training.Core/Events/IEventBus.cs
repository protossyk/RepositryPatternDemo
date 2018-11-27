using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    /// <summary>
    /// 1.发布
    /// 2.订阅
    /// </summary>
    public interface IEventBus
    {
        Task Publish(IEventData @event);

        void Subscribe<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>;


        void Unsubscribe<T, TH>()
            where TH : IEventHandler<T>
            where T : IEventData;
        bool IsSubscribed<T, TH>();
    }
}
