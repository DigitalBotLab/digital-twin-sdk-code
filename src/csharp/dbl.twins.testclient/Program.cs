using dbl.twins.sdk;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            TwinClient client = new TwinClient("Endpoint=sb://bldg-hubns-6uad.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WcB4E913CSmbtyJf7yJphWeIt5vc1Ex7C+AEhHZiW4k=", "bldg-hub-6uad");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}