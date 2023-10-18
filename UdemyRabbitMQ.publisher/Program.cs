using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://fygjaidn:ala5AqMZXx1zmbpu8iXNr4yPce_ikxVq@shrimp.rmq.cloudamqp.com/fygjaidn");

            using var connection=factory.CreateConnection();

            var channel=connection.CreateModel();

            //channel.QueueDeclare("hello-queue",true,false,false);

            channel.ExchangeDeclare("logs-fanout", durable: true,type:ExchangeType.Fanout);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"log {x}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("logs-fanout","", null, messageBody);

                Console.WriteLine($"Mesaj gönderilmiştir: {message}");


            });

            Console.ReadLine();
        }
    }
}
