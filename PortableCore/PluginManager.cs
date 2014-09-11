using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommonInterfaces.Base;
using PCLStorage;

namespace PortableCore
{
    public class PluginManager
    {
        public IPluginMaster Master { get; set; }

        [ImportMany]
        public IEnumerable<IPlugin> Plugins { get; set; } 

        public PluginManager(string pluginDirectory)
        {
            Master = new PluginMaster();
            RegisterPlugins(pluginDirectory);
        }

        private void RegisterPlugins(string pluginDirectory)
        {
            Plugins = new List<IPlugin>();

            try
            {
                var configuration = new ContainerConfiguration().WithAssembly(typeof(IPlugin).GetTypeInfo().Assembly);
                var compositionHost = configuration.CreateContainer();
                compositionHost.SatisfyImports(this);

                foreach (IPlugin plugin in Plugins)
                {
                    plugin.Master = Master;
                }
            }
            catch (Exception ex)
            {
                Master.Messages.Enqueue(ex.Message);
            }
        }

        private IEnumerable<Assembly> LoadAssemblies(string pluginDirectory)
        {
            List<Assembly> assemblies = new List<Assembly>();

            return assemblies;
        }

        public IEnumerable<T> GetPlugins<T>()
        {
            return Plugins.OfType<T>().ToList();
        }
    }
}
