using System.Collections.Generic;

namespace CommonInterfaces.Base
{
    public interface IPluginMaster
    {
        Queue<string> Messages { get; set; }
    }
}
