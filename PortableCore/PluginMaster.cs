using System.Collections.Generic;
using CommonInterfaces.Base;

namespace PortableCore
{
    public class PluginMaster : IPluginMaster
    {
        public Queue<string> Messages { get; set; }

        public PluginMaster()
        {
            Messages = new Queue<string>();
        }
    }
}
