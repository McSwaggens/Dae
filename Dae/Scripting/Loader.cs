using Dae.Plugin;
using Dae.Properties;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dae.Scripting
{
	internal static class Loader
	{
		public static string scriptPath = "";

		public static readonly bool replaceAlways = false;

		private static Script luaScript;

		public static bool Ready => luaScript != null;

		/// <summary>
		/// Finds the script on the computer and prepares it for runtime
		/// </summary>
		public static void PrepareScript ()
		{
			if (Ready)
			{
				Logger.Log ("Attempted to prepare already prepared Loader script");
				return;
			}

			// Find the script on the computer

			scriptPath = Util.HomeDirectory + "DLoader.lua";

			if (!File.Exists (scriptPath))
			{
				Logger.Log ("Loader script does not exist!\n\tCreating default DLoader.lua script now...");

				StreamWriter writer = File.CreateText (scriptPath);
				writer.Write (Resources.DLoader);
				writer.Close ();

				Logger.Log ("Created default DLoader.lua script.");
			}
			else if (replaceAlways)
			{
				File.Delete (scriptPath);
				File.WriteAllText (scriptPath, Resources.DLoader);

				Logger.Log ("[replaceAlways] Replaced DLoader.lua script with default one from Resources.");
			}

			luaScript = new Script (CoreModules.Basic | CoreModules.Math | CoreModules.OS_Time);

			foreach (DPluginDefinition definition in PluginSystem.definitions)
			{
				UserData.RegisterType (definition.plugin);
			}

			UserData.RegisterType (typeof (Canvas));
			UserData.RegisterType (typeof (Component));

			luaScript.Globals[nameof (ScriptInterface.LoadPlugin)] = (Func<string, DPlugin>)ScriptInterface.LoadPlugin;
			luaScript.Globals[nameof (ScriptInterface.CreateComponent)] = (Func<string, Component>)ScriptInterface.CreateComponent;

			luaScript.Globals["rootCanvas"] = Dae.RootCanvas;
		}

		internal static void Run ()
		{
			if (Ready)
			{
				try
				{
					luaScript.DoFile (scriptPath);
				}
				catch (ScriptRuntimeException e)
				{
					Logger.Log ($"Loader script execution failed\n\tError: {e.Message}");
				}
			}
		}

		[MoonSharpUserData]
		private static class ScriptInterface
		{
			public static Component CreateComponent ( string componentName )
			{
				DCustomComponentDefinition definition = PluginSystem.FindComponentDefinition (componentName);
				if (definition == null)
				{
					return null;
				}

				Component componentInstance = (Component)Activator.CreateInstance (definition.type);
				return componentInstance;
			}

			public static DPlugin LoadPlugin ( string pluginName )
			{
				DPlugin pluginInstance = PluginSystem.LoadPlugin (pluginName);

				return pluginInstance;
			}
		}
	}
}