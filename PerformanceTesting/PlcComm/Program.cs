using System;
using System.Threading.Tasks;

namespace AsyncPlcComm
{
    class Program
    {
        const int NumberOfExecutes = 10;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating PLC Engine");
            PlcEngine plcEngine = new PlcEngine();

            Console.WriteLine("Creating PLC Clients");
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTagOne", 300));
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTagTwo", 300));
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTagThree", 300));
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTagFour", 300));
            plcEngine.AddClient(new PlcClient("192.168.10.100", "AsyncTagFive", 300));

            for (int count = 0; count < NumberOfExecutes; count ++)
            {
                await plcEngine.RefreshAsync();
                var results = plcEngine.GetResults();
                // We would send results to the data engine here
            }

            plcEngine.Dispose();

            Console.WriteLine("Test Done");
        }
    }
}
