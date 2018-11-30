using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoHome.Training.Core.Events;
using AutoHome.Training.PortalDemo.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoHome.Training.PortalDemo.Pages
{
    public class CustomEventDemoModel : PageModel
    {
        private readonly IEventBus _eventBus;
        public CustomEventDemoModel(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public void OnGet()
        {
        }

        public async void OnPostAsync(int orderId, float money)
        {
            //todo
            //data save action

            //�˴�Demo��Push��Subscribe ����ͬһ������ʵ��ʹ���ǲ�ͬ�ķ���
            OrderConfirmedEventData orderConfirmedEventData = new OrderConfirmedEventData()
            {
                OrderId=orderId,
                Money=money
            };
            await _eventBus.Publish(orderConfirmedEventData);
        }
    }
}