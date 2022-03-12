using AutomatedSolutions.ASCommStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPlcComm
{
    public class PlcClient: IDisposable
    {
        private ASCommIot ASCommIot;
        private Channel Channel;
        private Device Device;
        private Group Group;
        private List<Item> Items;
        private List<Result> Results;

        public PlcClient(string route, string baseTagName, int numberOfTags)
        {
            Initialize(route, baseTagName, numberOfTags);
        }

        public void Dispose()
        {
            ASCommIot.Dispose();
        }

        public List<Result> GetResults()
        {
            return Results;
        }

        public void Initialize(string route, string baseTagName, int numberOfTags)
        {
            ASCommIot = new ASCommIot();
            Channel = new AutomatedSolutions.ASCommStd.AB.Logix.Net.Channel();
            Device = new AutomatedSolutions.ASCommStd.AB.Logix.Device(route, AutomatedSolutions.ASCommStd.AB.Logix.Model.ControlLogix);
            Group = new AutomatedSolutions.ASCommStd.AB.Logix.Group(false, 1000);
            
            Items = new List<Item>();
            Results = new List<Result>();

            for (int count = 0; count < numberOfTags; count++)
            {
                Item item = new AutomatedSolutions.ASCommStd.AB.Logix.Item($"{baseTagName}[{count}]");
                Group.Items.Add(item);
            }

            ASCommIot.Channels.Add(Channel);
            Channel.Devices.Add(Device);
            Device.Groups.Add(Group);
            
            foreach (var item in Items)
                Group.Items.Add(item);
        }

        public async Task RefreshAsync()
        {
            Results.Clear();
            //await Device.ReadAsync(Items, Results);
            await Task.Delay(1000);
        }
    }
}
