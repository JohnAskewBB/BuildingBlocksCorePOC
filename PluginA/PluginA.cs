using System.Composition;
using CommonInterfaces.Base;

namespace PluginA
{
    [Export(typeof(IPlugin))]
    public class PluginA : IPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public PluginA()
        {
            Name = "Plugin A";
            MajorVersion = 0;
            MinorVersion = 1;
        }
    }
}
