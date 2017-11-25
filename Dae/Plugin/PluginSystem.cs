using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dae.Plugin
{
	internal static class PluginSystem
	{
		internal static Dictionary<Type, DPluginInformation> discoveredPlugins = new Dictionary<Type, DPluginInformation> ();
		internal static List<DPlugin> activePlugins = new List<DPlugin> ();

		internal static void LoadPluginsFromAssembly ( Assembly assembly )
		{
			Type[] typesFromAsm = assembly.GetTypes ();

			foreach (Type type in typesFromAsm)
			{
				Attribute attribute = type.GetCustomAttribute (typeof (DPluginInformation), true);

				if (attribute != null)
				{
					DPluginInformation pluginInformation = (DPluginInformation)attribute;
					if (type.BaseType == typeof (DPlugin))
					{
						// Valid DAE Plugin!

						discoveredPlugins.Add (type, pluginInformation);

						Logger.Log ($"Loaded plugin from assembly '{assembly.FullName}':\n\tName: {pluginInformation.name}\n\tAuthor: {pluginInformation.author}\n\tDescription: {pluginInformation.description}");
					}
				}
			}
		}

		internal static Type FindPlugin ( string name )
		{
			foreach (KeyValuePair<Type, DPluginInformation> pluginPair in discoveredPlugins)
			{
				if (pluginPair.Value.name == name)
				{
					return pluginPair.Key;
				}
			}

			return null;
		}

		internal static DPluginInformation GetPluginInformation ( Type pluginType )
		{
			return discoveredPlugins[pluginType];
		}

		internal static void ActivatePlugin ( DPlugin plugin )
		{
			if (!activePlugins.Contains (plugin))
			{
				activePlugins.Add (plugin);
				plugin.Load ();

				Logger.Log ($"Activated plugin: {plugin.information.name}");
			}
		}

		internal static void DeActivatePlugin ( DPlugin plugin )
		{
			if (activePlugins.Contains (plugin))
			{
				activePlugins.Remove (plugin);
				plugin.Unload ();
			}
		}
	}
}