using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;

namespace SRMLExtras
{
	/// <summary>
	/// The main class and entry point for the mod
	/// </summary>
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		// PRE LOAD MOD
		public override void PreLoad()
		{
			// Generates the config file
			Configs.ConfigHandler.CopyFiles(Assembly.GetExecutingAssembly());

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		// POST LOAD MOD
		public override void PostLoad()
		{
		}
	}
}
