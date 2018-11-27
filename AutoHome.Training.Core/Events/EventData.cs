using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Core.Events
{
    public abstract class EventData:IEventData
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
