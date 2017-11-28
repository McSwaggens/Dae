-- ██████╗  █████╗ ███████╗
-- ██╔══██╗██╔══██╗██╔════╝
-- ██║  ██║███████║█████╗
-- ██║  ██║██╔══██║██╔══╝
-- ██████╔╝██║  ██║███████╗
-- ╚═════╝ ╚═╝  ╚═╝╚══════╝

-- This is the default DAE loader script
-- Feel free to modify this file all you like, add plugins, modify plugins etc...

-- Built in variables are as follows:
--	args
--	inputFile
--	plugins
--	rootCanvas

-- Built in functions are as follows:
--	LoadPlugin (pluginName)
--		- Loads the plugin into DAE and initializes it
--		- Returns an instance of the plugin,
--		- you're able to call functions within the plugin.
--
--	UnloadPlugin (pluginName or pluginInstance)
--		- Unloads the plugin from DAE
--	

-- Enjoy!

test = LoadPlugin ("TestPlugin");

test.HelloWorld();