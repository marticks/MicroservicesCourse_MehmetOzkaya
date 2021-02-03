using System;
using RabbitMQ.Client;

namespace src.Common.EventBusRabbitMQ
{
    public interface IRabbitMQConnection : IDisposable // es buena practica que conection type classes sean IDisposable
    {
       bool IsConnected {get;}
       bool TryConnect();
       IModel CreateModel(); 
    }
}