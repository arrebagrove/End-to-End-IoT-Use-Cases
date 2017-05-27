using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;


namespace SendCloudToDevice
{
    class Program
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=cts.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RLpl4TOC1dULFWZNEnvYy5rgOF2XH9kJwhNEH6n5JGY=";

        static void Main(string[] args)
        {
            Console.WriteLine("Send Cloud-to-Device message\n");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            while (true)
            {
                Console.WriteLine("Press any key to send a C2D message.");
                Console.ReadLine();
                SendCloudToDeviceMessageAsync().Wait();
            }
            //Console.ReadLine();
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            await serviceClient.SendAsync("myFirstDevice", commandMessage);
        }


    }
}
