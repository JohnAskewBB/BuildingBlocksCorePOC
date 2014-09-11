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

        private async void RegisterPlugins(string pluginDirectory)
        {
            Plugins = new List<IPlugin>();

            try
            {
                var configuration = new ContainerConfiguration().WithAssemblies(await LoadAssemblies(pluginDirectory));
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

        private async Task<IEnumerable<Assembly>> LoadAssemblies(string pluginDirectory)
        {
            List<Assembly> assemblies = new List<Assembly>();

            //IFolder folder = await FileSystem.Current.GetFolderFromPathAsync(pluginDirectory);
            //IList<IFile> files = await folder.GetFilesAsync();
            //foreach (IFile file in files)
            //{
            //    if (file.Name.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        Assembly assembly = Assembly.Load(new AssemblyName(file.Name));
            //        assemblies.Add(assembly);
            //    }
            //}

            return assemblies;
        }

        public IEnumerable<T> GetPlugins<T>()
        {
            return Plugins.OfType<T>().ToList();
        }
    }
}
