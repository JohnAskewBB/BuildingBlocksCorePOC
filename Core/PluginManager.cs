using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonInterfaces.Base;

namespace Core
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
                var configuration = new ContainerConfiguration().WithAssemblies(LoadAssemblies(pluginDirectory));
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

            DirectoryInfo directory = new DirectoryInfo(pluginDirectory);
            FileInfo[] files = directory.GetFiles("*.dll");
            foreach (FileInfo file in files)
            {
                Assembly assembly = Assembly.LoadFile(file.FullName);
                assemblies.Add(assembly);
            }

            return assemblies;
        }

        public IEnumerable<T> GetPlugins<T>()
        {
            return Plugins.OfType<T>().ToList();
        }
    }
}
