using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class RabbitMQService
    {
        public IConnection _conn { get; set; }
        public IModel _channel{ get; set; }
        public RabbitMQService()
        {
            Task.Delay(1000).Wait();
            Console.WriteLine("Consuming Queue Now");

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 }; // rabbitmqlooslycoupled.westeurope.azurecontainer.io on azure
            factory.UserName = "guest";
            factory.Password = "guest"; //n0m3ymXVqLLfLRLo on azure
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "alfamlooslycoupled",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);


        }

        public void PublishMessage(string message)
        {
            
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: "alfamlooslycoupled",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
