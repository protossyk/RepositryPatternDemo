using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoHome.Training.Core.Events
{
    public interface IEventHandler
    {
        Task<bool> HandleAsync(IEventData @event, CancellationToken cancellationToken = default);

        bool CanHandle(IEventData @event);
    }
    public interface IEventHandler<in TEvenetData>
    where TEvenetData : IEventData
    {
        /// <summary>
        /// 机器处理过程
        /// </summary>
        /// <param name="event">事件源数据</param>
        /// <returns>执行任务</returns>
        Task HandleAsync(TEvenetData @event, CancellationToken cancellationToken = default);
    }
}
