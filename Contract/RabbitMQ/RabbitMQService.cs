using Contract.Infra.Messaging;
using Newtonsoft.Json;
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
        readonly string ExchangeName = "alfam.fanout";
        readonly string QueueName = "alfam-contract";

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
            _channel.QueueDeclare(queue: QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueBind(QueueName, ExchangeName, "", null);
        }

        public void PublishObject<T>(T message) where T : Event
        {
            string jsonMessage = JsonConvert.SerializeObject(message);
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(exchange: ExchangeName,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        public void RegisterConsumer<T>(Action<T> consumer) where T : Event
        {
            var eventConsumer = new EventingBasicConsumer(_channel);
            eventConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                byte[] bytes = new byte[ea.Body.Length];
                body.CopyTo(bytes);
                var message = Encoding.UTF8.GetString(bytes);
                T result = JsonConvert.DeserializeObject<T>(message);
                if(result.MessageType == typeof(T).Name)
                {
                    Console.WriteLine(" [x] Received from Rabbit: {0}", message);
                    consumer.Invoke(result);
                }
            };
            _channel.BasicConsume(queue: QueueName,
                                    autoAck: true,
                                    consumer: eventConsumer);
        }
    }
}
