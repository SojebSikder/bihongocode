using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bihongoPlugin;
using SimplePlugin;

namespace bihongoCode.library
{
    class PluginUtility
    {
        public static Dictionary<string, StandardIO> _StandardIOPlugins;

        /**
         * Get Plugin Version
         */
        public static dynamic getPlugin_Property(StandardIO item, string plugin_property)
        {
            dynamic version = null;
            dynamic devType = item.GetType();
            dynamic dev = Activator.CreateInstance(devType);

            dynamic property = devType.GetProperty(plugin_property);

            if (property != null)
            {
                version = property.GetValue(dev);
                return version;
            }
            else
            {
                version = null;
                return version;
            }
        }

        /**
         * Get Plugin list
         */
        public static ICollection<StandardIO> PluginList()
        {
            _StandardIOPlugins = new Dictionary<string, StandardIO>();
            ICollection<StandardIO> StandardIOPlugins = PluginLoader.LoadDevPlugins("Plugins");
            return StandardIOPlugins;
        }


        /**
         * Get plugin property by key and return version
         */
        public static dynamic getPlugin_PropertyByKey(string key, string plugin_property)
        {
            dynamic version = null;

            PluginUtility._StandardIOPlugins.ContainsKey(key);
            StandardIO plugin = PluginUtility._StandardIOPlugins[key];

            dynamic devType = plugin.GetType();
            dynamic dev = Activator.CreateInstance(devType);

            dynamic property = devType.GetProperty(plugin_property);

            if (property != null)
            {
                version = property.GetValue(dev);
                return version;
            }
            else
            {
                version = null;
                return version;
            }
        }


    }
}
