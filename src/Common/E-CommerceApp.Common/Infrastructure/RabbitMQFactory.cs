using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Infrastructure
{
    public static class RabbitMQFactory
    {
        private static EventingBasicConsumer _consumer;

        private static object lockObject = new object();

        public static EventingBasicConsumer Consumer
        {
            get
            {
                lock (lockObject)
                {
                    if (_consumer == null)
                    {
                        _consumer = CreateBasicConsumer();
                    }
                    return _consumer;
                }
            }
        }

        public static void SendMessageToQueue(string exchangeName,
            string exchangeType,
            string queueName,
            object obj)
        {
            var channel = Consumer
                .EnsureExchange(exchangeName, exchangeType)
                .EnsureQueue(queueName, exchangeName)
                .Model;
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: queueName,
                basicProperties: null,
                body: body);
        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = AppConstants.RabbitMQHost
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,
                                                           string exchangeName,
                                                           string exchangeType = AppConstants.DefaultExhangeType)
        {
            consumer.Model.ExchangeDeclare(exchange: exchangeName,
                                           type: exchangeType,
                                           durable: false,
                                           autoDelete: false);
            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                           string queueName,
                                                           string exchangeName)
        {
            consumer.Model.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        null);

            consumer.Model.QueueBind(queueName, exchangeName, queueName);

            return consumer;
        }

        public static EventingBasicConsumer Receive<T>(this EventingBasicConsumer consumer,
            Action<T> action)
        {
            consumer.Received += (m, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var model = JsonSerializer.Deserialize<T>(message);

                action(model);
                consumer.Model.BasicAck(eventArgs.DeliveryTag, false);
            };

            return consumer;
        }

        public static EventingBasicConsumer StartConsuming(this EventingBasicConsumer consumer, string queueName)
        {
            consumer.Model.BasicConsume(queue: queueName,
                                        autoAck: false,
                                        consumer: consumer);

            return consumer;
        }
    }
}
