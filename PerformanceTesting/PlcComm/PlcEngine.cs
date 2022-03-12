using AutomatedSolutions.ASCommStd;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPlcComm
{
    public class PlcEngine: IDisposable
    {
        private List<PlcClient> PlcClients;
        private List<Task> PlcTasks;
        private Stopwatch EngineStopwatch;
        private List<Result> Results;

        public PlcEngine()
        {
            PlcClients = new List<PlcClient>();
            PlcTasks = new List<Task>();
            EngineStopwatch = new Stopwatch();
            Results = new List<Result>();
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

        public List<Result> GetResults()
        {
            Results.Clear();

            foreach (var client in PlcClients)
                Results.Concat(client.GetResults());

            return Results;
        }

        public async Task RefreshAsync()
        {
            EngineStopwatch.Start();

            foreach (var plcClient in PlcClients)
                PlcTasks.Add(plcClient.RefreshAsync());

            await Task.WhenAll(PlcTasks);

            PlcTasks.Clear();

            EngineStopwatch.Stop();
            Console.WriteLine($"Engine took {EngineStopwatch.ElapsedMilliseconds}ms for {PlcClients.Count} clients.");
            EngineStopwatch.Reset();
        }
    }
}
