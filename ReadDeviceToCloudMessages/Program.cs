using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;

namespace ReadDeviceToCloudMessages
{
    class Program
    {
        static string connectionString = "HostName=cts.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RLpl4TOC1dULFWZNEnvYy5rgOF2XH9kJwhNEH6n5JGY=";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        

        static void Main(string[] args)
        {
            var  hubConnection = new HubConnection("http://localhost:8080/");
            IHubProxy myHubProxy = hubConnection.CreateHubProxy("MyHub");
            hubConnection.Start();

            Console.WriteLine("Receive messages. Ctrl-C to exit.\n");
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, myHubProxy, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());

        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition, IHubProxy hubProxy, CancellationToken ct)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                await hubProxy.Invoke("Send", new object[] { (object) "Partition: " + partition, (object) data });
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
            }
        }

    }
}
