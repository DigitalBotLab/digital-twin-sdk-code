using dbl.twins.sdk;
using System.Net;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            TwinClient client = new TwinClient("Endpoint=sb://twd-twinns-r5wn.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=cjAnD4wBcR7rMi2slD1znRZBJK6ZW7e/K+AEhKXgp/g=;EntityPath=twd-twinhub-r5wn", "twd-twinhub-r5wn");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}
