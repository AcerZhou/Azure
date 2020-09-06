using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPublisher
{
    public class Program
    {
        private const string topicEndPoint = "<topic_endpoint>";
        private const string topicKey = "<topic_key>";

        public static async Task Main(string[] args)
        {
            var credentials = new TopicCredentials(topicKey);
            var client = new EventGridClient(credentials);

            var events = new List<EventGridEvent>();

            var firstPerson = new 
            {
                FullName = "Alba Sutton",
                Address = "123 Pine Avenue, WA 97202"
            };

            var firstEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {firstPerson.FullName}",
                Data = firstPerson.ToString(),
                DataVersion = "1.0.0"
            };

            events.Add(firstEvent);

            var secondPerson = new 
            {
                FullName = "Alba Sutton",
                Address = "123 Pine Avenue, WA 97202"
            };

            var secondEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {secondPerson.FullName}",
                Data = secondPerson.ToString(),
                DataVersion = "1.0.0"
            };

            events.Add(secondEvent);

            var topicHostName = new Uri(topicEndPoint).Host;
            await client.PublishEventsAsync(topicHostName, events);

            Console.WriteLine("Events Published");
        }
    }
}
