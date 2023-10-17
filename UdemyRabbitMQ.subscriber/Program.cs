using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace UdemyRabbitMQ.subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://fygjaidn:ala5AqMZXx1zmbpu8iXNr4yPce_ikxVq@shrimp.rmq.cloudamqp.com/fygjaidn");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();


            // Eğer publisher tarafında bu kuyruk varsa tekrardan declare etmeye gerek yok.Varsa da publisher ve subscriber parametreleri aynı olmalı
            //channel.QueueDeclare("hello queue", true, false, false);

            var consumer=new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue", true, consumer);


            // AŞAĞIDAKİ PRİVATE METODUN KISA HALİ

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message=Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:"+message);
            };
            

            Console.ReadLine();
        }

        //private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
