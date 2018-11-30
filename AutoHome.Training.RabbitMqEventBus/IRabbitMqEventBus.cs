using System.Threading.Tasks;
using AutoHome.Training.Core.Events;

namespace AutoHome.Training.RabbitMq
{
    public interface IRabbitMqEventBus
    {
        Task Publish(IEventData @event);
        void Subscribe<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>;
        void Unsubscribe<T, TH>()
            where T : IEventData
            where TH : IEventHandler<T>;
    }
}