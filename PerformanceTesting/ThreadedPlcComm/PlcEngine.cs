using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedPlcComm
{
    public class PlcEngine: IDisposable
    {
        private List<PlcClient> PlcClients;
        private List<Task> PlcTasks;

        public PlcEngine()
        {
            PlcClients = new List<PlcClient>();
            PlcTasks = new List<Task>();
        }

        public void AddClient(PlcClient asyncPlcClient)
        {
            PlcClients.Add(asyncPlcClient);
        }

        public void Dispose()
        {
            foreach (var client in PlcClients)
                client.Dispose();
        }

        public void Start()
        {
            foreach (var client in PlcClients)
                client.Start();
        }

        public void Stop()
        {
            foreach (var client in PlcClients)
                client.Stop();
        }
    }
}
