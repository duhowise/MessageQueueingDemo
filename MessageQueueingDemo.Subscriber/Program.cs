using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageQueueingDemo.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

          channel.QueueDeclare("MessageQueueingDemoQueue", true, false, false, null);

          var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model,eventArgs)=>
            {
                var body = eventArgs.Body;
                Console.WriteLine($"Received Message: {Encoding.UTF8.GetString(body)}");
                Thread.Sleep(100);
                channel.BasicAck(eventArgs.DeliveryTag,false);
            };

            channel.BasicConsume( "MessageQueueingDemoQueue",  false, consumer);

            Console.WriteLine("now waiting for messages.......");
            Console.ReadLine();

        }

        
    }
}
