using System;

namespace ipcheck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                System.Console.WriteLine("Current IP Address");

                var hostname = System.Net.Dns.GetHostName();
                var host = System.Net.Dns.GetHostEntry(hostname);

                foreach(var address in host.AddressList)
                {
                    System.Console.WriteLine($"\t{address}");
                }
            }
            else
            {
                System.Console.WriteLine("No Network Connection");
            }
        }
    }
}
