using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace ServiceBus
{
    class Program
    {
        private const string ServiceBusConnectionString = "<service_bus_connection_string>";
        private const string QueueName = "<queue_name>";
        public static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            await SendMessageAsync(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();

            Console.WriteLine("Hello World!");
        }

        public static async Task SendMessageAsync(int numberOfMessages)
        {
            try
            {
                for (var i = 0; i< numberOfMessages; i++)
                {
                    var messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    Console.WriteLine($"Sending message: {messageBody}");

                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {ex.Message}");
            }
        }


    }
}
