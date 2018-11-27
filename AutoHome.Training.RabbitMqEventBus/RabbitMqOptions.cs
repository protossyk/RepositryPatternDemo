using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.RabbitMq
{
    public class RabbitMqOptions : IOptions<RabbitMqOptions>
    {
        public RabbitMqOptions Value => this;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
