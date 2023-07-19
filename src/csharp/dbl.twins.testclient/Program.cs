using dbl.twins.sdk;
using System.Net;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            TwinClient client = new TwinClient("Endpoint=sb://tnf-twinns-6sc4.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=lZ2xxrzE7zZ9AYE7YOY38Z6IWM7aQ7qqH+AEhM9o1j4=;EntityPath=tnf-twinhub-6sc4");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}
