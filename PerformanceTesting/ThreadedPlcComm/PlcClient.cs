using AutomatedSolutions.ASCommStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ThreadedPlcComm
{
    public class PlcClient: IDisposable
    {
        private ASCommIot ASCommIot;
        private Channel Channel;
        private Device Device;
        private Group Group;
        private List<Item> Items;
        private List<Result> Results;

        private readonly Timer PollTimer;

        public PlcClient(string route, string baseTagName, int numberOfTags)
        {
            Initialize(route, baseTagName, numberOfTags);

            PollTimer = new Timer(1000);
            PollTimer.Elapsed += PollTimer_Elapsed;
        }

        public void Dispose()
        {
            PollTimer.Dispose();
            ASCommIot.Dispose();
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

        private void PollTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            Results.Clear();
            //await Device.ReadAsync(Items, Results);
            Task.Delay(1000);
        }

        public void Start()
        {
            PollTimer.Start();
        }

        public void Stop()
        {
            PollTimer.Stop();
        }
    }
}
