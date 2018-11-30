using AutoHome.Training.Core.Events;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Training.RabbitMq
{
    public class RabbitMqEventBus : BaseEventBus, IRabbitMqEventBus
    {
        private readonly IEventExecutContext _eventExecutContext;
        private bool disposed = false;
        const string BROKER_NAME = "autohome_training_event_bus";
        const string ExceptionQueue = "autohome_training_error_queue";

        private readonly IBus _ibus;
        private readonly ILogger<RabbitMqEventBus> _logger;
        private IBinding _iBinding { get; set; }
        private string _queueName;
        public RabbitMqEventBus(
            IEventExecutContext eventSubscriptionManger,
            ILogger<RabbitMqEventBus> logger,
            IBus bus,
            string queueName = null, 
            int retryCount = 5
            )
            : base(eventSubscriptionManger)
        {
            _eventExecutContext = eventSubscriptionManger;
            _logger = logger;
            _ibus = bus;

            _eventExecutContext.OnEventRemoved += SubsManager_OnEventRemoved;
            _queueName = InitConsumer(queueName);
        }
        public override Task Publish(IEventData @event)
        {
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            MessageProperties messageProperties = new MessageProperties();
            IExchange exchange = _ibus.Advanced.ExchangeDeclare(BROKER_NAME, "topic");
            _logger.LogDebug($"exchange:{exchange.Name},routekey:{@event.GetType().Name},message:{message}");
            return _ibus.Advanced.PublishAsync(exchange, @event.GetType().Name, true, messageProperties, body);
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (_iBinding != null)
            {
                _ibus.Advanced.BindingDelete(_iBinding);
            }

            if (_eventExecutContext.IsEmpty)
            {
                _queueName = string.Empty;
                _ibus.Dispose();
            }
        }
        private string InitConsumer(string queueName)
        {
            IQueue queue = null;
            if (string.IsNullOrEmpty(queueName))
            {
                queue = _ibus.Advanced.QueueDeclare();
            }
            else
            {
                queue = _ibus.Advanced.QueueDeclare(queueName);//此处只是GetQueue，但无此方法
            }
            try
            {
                //消费接收消息的操作定义
                //如果Consume，不发生异常，则EasyNetQ会向服务器发送ACK，所以不要自己捕获异常
                _ibus.Advanced.Consume(queue, async (messageBody, iMessage, messageReceive) =>
                {
                    var message = Encoding.UTF8.GetString(messageBody);
                    _logger.LogDebug($@"queue:{_queueName},message:{message},
                                    messageReceive.RoutingKey:{messageReceive.RoutingKey}");
                    var eventType = _eventExecutContext.GetEventTypeByName(messageReceive.RoutingKey);
                    var integrationEvent = (IEventData)JsonConvert.DeserializeObject(message, eventType);
                    await _eventExecutContext.HandleAsync(integrationEvent);
                });
            }
            catch (EasyNetQException ex)
            {
                //处理连接消息服务器异常
                _logger.LogError($"message:{ex.Message},stacktrace:{ex.StackTrace}");
            }
            return queue.Name;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_ibus != null)
                {
                    _ibus.Dispose();
                }

                _eventExecutContext.Clear();

            }
            disposed = true;
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = _eventExecutContext.GetEventKey<T>();
            BindRabbitMqSubscription(eventName);
            base.Subscribe<T, TH>();
        }

        public override void Unsubscribe<T, TH>()
        {
            UnBindRabbitMqSubscription(_iBinding);
            base.Unsubscribe<T, TH>();
        }
        /// <summary>
        /// RabbitMq的订阅实现（queue bind exchange's routekey）
        /// </summary>
        /// <param name="eventName"></param>
        private void BindRabbitMqSubscription(string eventName)
        {
            var containsKey = _eventExecutContext.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                IQueue queue = _ibus.Advanced.QueueDeclare(_queueName);
                IExchange exchange = _ibus.Advanced.ExchangeDeclare(BROKER_NAME, "topic");
                _iBinding = _ibus.Advanced.Bind(exchange, queue, eventName);
            }
        }
        private void UnBindRabbitMqSubscription(IBinding binding)
        {
           _ibus.Advanced.BindingDelete(binding);
        }
    }
}
