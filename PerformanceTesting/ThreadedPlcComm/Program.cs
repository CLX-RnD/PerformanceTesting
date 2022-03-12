using System;
using System.Threading.Tasks;

namespace ThreadedPlcComm
{
    class Program
    {
        const int TimeToWait = 30000;

        static async Task Main(string[] args)
        {
           Console.WriteLine("Creating PLC Engine");
            PlcEngine plcEngine = new PlcEngine();

            Console.WriteLine("Creating PLC Clients");
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTag", 100));

            plcEngine.Start();

            await Task.Delay(TimeToWait);

            plcEngine.Stop();
            plcEngine.Dispose();

            Console.WriteLine("Test Done");
        }
    }
}
