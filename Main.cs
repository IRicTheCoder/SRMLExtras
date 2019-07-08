using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace SRML
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
			// Initializes the Console
			FileLogger.Init();
			Console.Init();

			// Config Handler


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
