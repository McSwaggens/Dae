using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dae.Plugin
{
	internal static class PluginSystem
	{
		internal static List<DPluginDefinition> definitions = new List<DPluginDefinition> ();
		internal static List<DPlugin> activePlugins = new List<DPlugin> ();

		internal static void LoadPluginsFromAssembly ( Assembly assembly )
		{
			Type[] typesFromAsm = assembly.GetTypes ();

			DPluginDefinition definition = null;

			bool foundPlugin = false;

			foreach (Type type in typesFromAsm)
			{
				Attribute attribute = type.GetCustomAttribute (typeof (DPluginInformation), true);

				if (attribute != null)
				{
					DPluginInformation pluginInformation = (DPluginInformation)attribute;
					if (type.BaseType == typeof (DPlugin))
					{
						// Valid DAE Plugin!

						foundPlugin = true;

						definition = new DPluginDefinition (pluginInformation, type);

						Logger.Log ($"Loaded plugin from assembly '{assembly.FullName}':\n\tName: {pluginInformation.name}\n\tAuthor: {pluginInformation.author}\n\tDescription: {pluginInformation.description}");

						break;
					}
				}
			}

			if (foundPlugin && definition != null)
			{
				foreach (Type type in typesFromAsm)
				{
					Attribute attribute = type.GetCustomAttribute (typeof (DCustomComponent), true);

					if (attribute != null)
					{
						DCustomComponent customComponent = (DCustomComponent)attribute;
						if (type.IsSubclassOf (typeof (Component)))
						{
							DCustomComponentDefinition componentDefinition = new DCustomComponentDefinition (customComponent, type);

							definition.customComponents.Add (componentDefinition);

							Logger.Log ($"Loaded custom component from assembly '{assembly.FullName}':\n\tActual Name: {customComponent.name}\n\tUsable Name: {definition.information.name}.{customComponent.name}");
						}
					}
				}
			}

			if (definition != null)
			{
				definitions.Add (definition);
			}
		}

		/// <summary>
		/// Finds the plugin type with a given name
		/// </summary>
		/// <param name="name">Desired plugin name</param>
		/// <returns>Plugin type, null if it wasn't found</returns>
		internal static DPluginDefinition FindPlugin ( string name )
		{
			foreach (DPluginDefinition definition in definitions)
			{
				if (definition.information.name == name)
				{
					return definition;
				}
			}

			return null;
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
			DPluginDefinition pluginDefinition = FindPlugin (pluginName);

			// Create an instance of the plugin
			DPlugin plugin = (DPlugin)Activator.CreateInstance (pluginDefinition.plugin);

			// Assign the information to the plugin object
			plugin.information = pluginDefinition.information;

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