using AutoHome.Training.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHome.Training.PortalDemo.Events
{
    public class OrderConfirmedEventData: EventData
    {
        public int OrderId { get; set; }
        public float Money { get; set; }
    }
}
