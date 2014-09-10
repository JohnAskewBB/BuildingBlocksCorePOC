using System.Composition;
using CommonInterfaces.Base;
using CommonInterfaces.Base.Test;

namespace PluginBandC
{
    [Export(typeof(IPlugin))]
    public class PluginC : ITestPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public PluginC()
        {
            Name = "Plugin C";
            MajorVersion = 0;
            MinorVersion = 1;
        }

        public void AddMessage(string message)
        {
            Master.Messages.Enqueue(message);
        }
    }
}
