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

            channel.BasicQos(0, 1, false); // true,her bir subscriber a ortadaki.değer/subscriber tane.
                                           // true değeri false olursa ortadaki sayı herbir subscriber a kaç tane değer gönderileceğini                 söyler.Örneğin 50 mesaj varsa 1'er,3'er yollar vb.

            var consumer=new EventingBasicConsumer(channel);

            // false olunca kuyruktan mesajı hemen silme

            channel.BasicConsume("hello-queue", false, consumer);


            // AŞAĞIDAKİ PRİVATE METODUN KISA HALİ

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message=Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:"+message);
                // bana ulaştırılan mesajı hangi tagla oluşturmuşsa kuyruktan siler. e.DeliveryTag
                // false değeri ilgili mesajın durumunu rabbitmq ya bildir

                channel.BasicAck(e.DeliveryTag, false);
            };
            

            Console.ReadLine();
        }

        //private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
