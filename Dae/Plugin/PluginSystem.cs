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

		/// <summary>
		/// Finds the plugin type with a given name
		/// </summary>
		/// <param name="name">Desired plugin name</param>
		/// <returns>Plugin type, null if it wasn't found</returns>
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

		internal static void UnloadPlugin ( DPlugin plugin )
		{
			if (activePlugins.Contains (plugin))
			{
				activePlugins.Remove (plugin);
			}

			plugin.Unload ();
		}

		internal static DPlugin LoadPlugin ( string pluginName )
		{
			Logger.Log ($"Attempting to load plugin: {pluginName}");

			// Get the plugin type by name
			Type pluginType = FindPlugin (pluginName);

			// Create an instance of the plugin
			DPlugin plugin = (DPlugin)Activator.CreateInstance (pluginType);

			// Get the plugin information
			DPluginInformation pluginInformation = GetPluginInformation (pluginType);

			// Assign the information to the plugin object
			plugin.information = pluginInformation;

			// Call the plugins load function
			bool loadedSuccessfully = false;

			try
			{
				loadedSuccessfully = plugin.Load ();
			}
			catch (Exception)
			{
				loadedSuccessfully = false;
			}

			if (loadedSuccessfully)
			{
				activePlugins.Add (plugin);
				Logger.Log ($"Activated plugin: {plugin.information.name}");
			}
			else
			{
				Logger.Warning ($"Unable to load plugin: {plugin.information.name}");
			}

			return plugin;
		}
	}
}