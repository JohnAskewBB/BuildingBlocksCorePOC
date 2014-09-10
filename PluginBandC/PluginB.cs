using System.Composition;
using CommonInterfaces.Base;

namespace PluginBandC
{
    [Export(typeof(IPlugin))]
    public class PluginB : IPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public PluginB()
        {
            Name = "Plugin B";
            MajorVersion = 0;
            MinorVersion = 1;
        }
    }
}
