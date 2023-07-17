using dbl.twins.sdk.std;
using System.Net;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            TwinClient client = new TwinClient("Endpoint=sb://tnd-twinns-bsta.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=4saM+TNagFu5uVCX8bVK6S+SDc937N9F5+AEhOXQJ/0=;EntityPath=tnd-twinhub-bsta");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}
