using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

namespace MessageProcessor
{
    public class Program
    {
        private const string storageConnectionString = "<storage_connection_string";
        private const string queueName = "<storage_queue_name>";

        public static async Task Main(string[] args)
        {
            var client = new QueueClient(storageConnectionString, queueName);
            await client.CreateAsync();

            Console.WriteLine("---Account Metadata");
            Console.WriteLine($"Account Uri:\t{client.Uri}");

            var greeting = "Hi, Devs ";
            await client.SendMessageAsync(greeting);

            Console.WriteLine("---Existing Messages");
            var batchSize = 10;
            var visibilityTimeout = TimeSpan.FromSeconds(2.5d);

            var messages = await client.ReceiveMessagesAsync(batchSize, visibilityTimeout);
            foreach(var message in messages?.Value)
            {
                Console.WriteLine($"[{message.MessageId}]\t{message.MessageText}");
                await client.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }
        

        }
    }
}
