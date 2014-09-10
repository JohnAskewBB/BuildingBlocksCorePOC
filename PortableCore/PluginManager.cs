using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
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

            //IFolder folder = await FileSystem.Current.GetFolderFromPathAsync(pluginDirectory);
            //IList<IFile> files = await folder.GetFilesAsync();
            //foreach (IFile file in files)
            //{
            //    if (file.Name.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        Assembly assembly = Assembly.Load(await file.ReadAllTextAsync());
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
