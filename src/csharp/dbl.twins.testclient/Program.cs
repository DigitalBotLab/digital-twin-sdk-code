using dbl.twins.sdk;
using System.Net;

namespace dbl.twins.testclient
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            TwinClient client = new TwinClient("");
            Task task = client.ConnectHub();
            task.Wait();
            //Console.ReadLine();

        }
    }
}
