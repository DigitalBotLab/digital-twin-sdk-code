using dbl.twins.sdk;
using System.Net;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            TwinEHClient client = new TwinEHClient("Endpoint=sb://aaa-twinns-slqs.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=lSJFfCaMenhGNpvuKPuniG3vKKijasPge+AEhCwJhjk=;EntityPath=aaa-twinhub-slqs");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}
