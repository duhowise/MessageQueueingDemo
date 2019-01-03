using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MessageQueueingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var bus=RabbitHutch.CreateBus("localhost"))
            //{
            //    Console.WriteLine("Hello World!");
            //}

            var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.ExchangeDeclare("MessageQueueingDemoExchange",ExchangeType.Direct);
            channel.QueueDeclare("MessageQueueingDemoQueue", true, false, false, null);
            channel.QueueBind("MessageQueueingDemoQueue", "MessageQueueingDemoExchange","RoutingKey",null);

            while (true)
            {
                var message = new MyMessage
                {
                    Name =$"Duhp{new Random().Next(10, 50)}",
                    Address = "56 Independence Avenue",
                    ShoeSize =new Random().Next(10,50)
                };
                var messageString = JsonConvert.SerializeObject(message);
                var messageBytes = Encoding.UTF8.GetBytes(messageString);
                channel.BasicPublish("MessageQueueingDemoExchange", "RoutingKey",null, messageBytes);
                Console.WriteLine($"published {message}");
            }
            

            channel.Dispose();
            connection.Dispose();
            Console.ReadKey();


        }
    }

    class MyMessage
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int ShoeSize { get; set; }
    }
}
