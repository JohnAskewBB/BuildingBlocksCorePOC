using System.Configuration;
using CommonInterfaces.Base;
using CommonInterfaces.Base.Test;
using Core;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string pluginDirectory = ConfigurationManager.AppSettings["pluginDirectory"];
            PluginManager manager = new PluginManager(pluginDirectory);

            foreach (IPlugin plugin in manager.GetPlugins<IPlugin>())
            {
                manager.Master.Messages.Enqueue("Loaded: " + plugin.Name + " (v" + plugin.MajorVersion + "." + plugin.MinorVersion + ")");
            }

            foreach (ITestPlugin plugin in manager.GetPlugins<ITestPlugin>())
            {
                plugin.AddMessage("Message added by " + plugin.Name + " (v" + plugin.MajorVersion + "." + plugin.MinorVersion + ") via AddMessage method.");
            }

            foreach (string message in manager.Master.Messages)
            {
                System.Console.WriteLine(message);
            }

            System.Console.ReadKey();
        }
    }
}
