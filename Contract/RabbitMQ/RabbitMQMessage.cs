using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.RabbitMQ
{
    public class RabbitMQMessage<T>
    {
        public RabbitMQMessage(Type originalObject, T content)
        {
            OriginalType = originalObject;
            Content = content;
        }

        public Type OriginalType { get; set; }
        public T Content { get; set; }
    }
}
