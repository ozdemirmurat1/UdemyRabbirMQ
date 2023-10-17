using RabbitMQ.Client;
using System;
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

            channel.QueueDeclare("hello-queue",true,false,false);

            string message = "hello world";

            var messageBody=Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);

            Console.WriteLine("Mesaj gönderilmiştir");

            Console.ReadLine();
        }
    }
}
