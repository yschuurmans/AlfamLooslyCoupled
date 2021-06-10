﻿using Contract.RabbitMQ;
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

        public void PublishObject<T>(T message)
        {
            var rabbitMessage = new RabbitMQMessage<T>(message.GetType(), message);
            string jsonMessage = JsonConvert.SerializeObject(rabbitMessage);
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(exchange: "",
                                 routingKey: "alfamlooslycoupled",
                                 basicProperties: null,
                                 body: body);
        }

        public void RegisterConsumer<T>(Action<T> consumer)
        {
            var eventConsumer = new EventingBasicConsumer(_channel);
            eventConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                byte[] bytes = new byte[ea.Body.Length];
                body.CopyTo(bytes);
                var message = Encoding.UTF8.GetString(bytes);
                RabbitMQMessage<T> result = JsonConvert.DeserializeObject<RabbitMQMessage<T>>(message);
                if(result.OriginalType.Name == typeof(T).Name)
                {
                    Console.WriteLine(" [x] Received from Rabbit: {0}", message);
                    consumer.Invoke(result.Content);
                }
            };
            _channel.BasicConsume(queue: "alfamlooslycoupled",
                                    autoAck: true,
                                    consumer: eventConsumer);
        }
    }
}