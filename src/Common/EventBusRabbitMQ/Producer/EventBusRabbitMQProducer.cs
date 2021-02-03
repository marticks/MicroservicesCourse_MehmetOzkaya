using System;
using System.Text;
using System.Text.Json;
using Events;
using RabbitMQ.Client;
using src.Common.EventBusRabbitMQ;

namespace Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            //esto supuestamente esta piola en la docu de rabbitmq
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonSerializer.Serialize(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange:"",routingKey:queueName, mandatory:true, basicProperties:properties, body:body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender,EventArgs) => {
                    Console.WriteLine("Sent RabbitMQ");
                    //implement ack handle
                };
                channel.ConfirmSelect();

            }
        }

    }
}