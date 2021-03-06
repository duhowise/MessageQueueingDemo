﻿using System;
using System.Text;
using System.Threading;
using EasyNetQ;
using MessageQueueingDemo.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageQueueingDemo.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            #region RabbitMqClient

            //  var factory = new ConnectionFactory
            //  {
            //      UserName = "guest",
            //      Password = "guest",
            //      HostName = "localhost"
            //  };
            //  var connection = factory.CreateConnection();
            //  var channel = connection.CreateModel();

            //channel.QueueDeclare("MessageQueueingDemoQueue", true, false, false, null);

            //var consumer = new EventingBasicConsumer(channel);
            //  consumer.Received += (model,eventArgs)=>
            //  {
            //      var body = eventArgs.Body;
            //      Console.WriteLine($"Received Message: {Encoding.UTF8.GetString(body)}");
            //      Thread.Sleep(100);
            //      channel.BasicAck(eventArgs.DeliveryTag,false);
            //  };

            //  channel.BasicConsume( "MessageQueueingDemoQueue",  false, consumer);

            //  Console.WriteLine("now waiting for messages.......");
            //  Console.ReadLine();

            #endregion

            //Subscribe();
            Receive();
        }

        private static void Receive()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
           
                bus.Receive("My.Queue",
                    x => x.Add<MyMessage>(m =>
                        {
                            Console.WriteLine($"Received {nameof(MyMessage)}  {m.Name} {m.ShoeSize}");
                        })
                        .Add<MyOtherMessage>(m =>
                        {
                            Console.WriteLine($"Received {nameof(MyOtherMessage)}  {m.Address} {m.Taxes}");
                        }));
           
        }

        private static void Subscribe()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Subscribe<MyMessage>("sub_id", x =>
            {
                Console.WriteLine($"Received Message{x.Name} {x.ShoeSize}");
            });
        }
    }
}
