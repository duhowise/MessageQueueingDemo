using System;
using System.Text;
using System.Threading;
using EasyNetQ;
using MessageQueueingDemo.Messages;
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

            #region RabbitMqClient

            //var factory = new ConnectionFactory
            //{
            //    UserName = "guest",
            //    Password = "guest",
            //    HostName = "localhost"
            //};

            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //var properties = channel.CreateBasicProperties();
            //properties.DeliveryMode = 2;

            //channel.ExchangeDeclare("MessageQueueingDemoExchange",ExchangeType.Direct);
            //channel.QueueDeclare("MessageQueueingDemoQueue", true, false, false, null);
            //channel.QueueBind("MessageQueueingDemoQueue", "MessageQueueingDemoExchange","RoutingKey",null);

            //while (true)
            //{
            //    var message = new MyMessage
            //    {
            //        Name =$"Duhp{new Random().Next(10, 50)}",
            //        Address = "56 Independence Avenue",
            //        ShoeSize =new Random().Next(10,50)
            //    };
            //    var messageString = JsonConvert.SerializeObject(message);
            //    var messageBytes = Encoding.UTF8.GetBytes(messageString);
            //    channel.BasicPublish("MessageQueueingDemoExchange", "RoutingKey",null, messageBytes);
            //    Console.WriteLine($"published {message}");
            //}
            

            //channel.Dispose();
            //connection.Dispose();
            //Console.ReadKey();

            #endregion

            //Publish();


            Send();
        }

        private static void Send()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            
                var rand=new Random();
                var message = new MyMessage
                {
                    Name = $"User-{rand.Next(1, 200)}",
                    ShoeSize = rand.Next(1, 50),
                    Address = $"{rand.Next(1, 200)}Next Lane"
                };
                var myOtherMessage = new MyOtherMessage
                {

                    Address = $"{rand.Next(1, 200)}Next Lane",
                    Taxes = Convert.ToDecimal(rand.NextDouble())
                };

                bus.Send("My.Queue",message);
                bus.Send("My.Queue",myOtherMessage);
                Console.WriteLine("sent two different messages....");
                Thread.Sleep(1000);
            
        }


        static void Publish()
        {
            var rand=new Random();
            var bus = RabbitHutch.CreateBus("host=localhost");
            while (true)
            {
                var message = new MyMessage
                {
                    Name = $"User-{rand.Next(1, 200)}",
                    ShoeSize = rand.Next(1, 50),
                    Address = $"{rand.Next(1, 200) }Next Lane"
                };
                bus.Publish(message);
                Console.WriteLine($"Published Message for{message.Name}");
                Thread.Sleep(1000);
            }
        }
    }
}
