using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;
using SRMLExtras.Templates;
using SRML.Console;
using SRML.Utils;
using UnityEngine;
using SRML.Utils.Enum;
using SRML.SR;

namespace SRMLExtras
{
	/// <summary>
	/// The main class and entry point for the mod
	/// </summary>
	[EnumHolder]
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		private static LookupDirector Lookup => GameContext.Instance.LookupDirector;

		// PRE LOADS MOD
		public override void PreLoad()
		{
			Console.RegisterCommand(new Debug.DebugCommand());
			//DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		// LOADS MOD
		public override void Load()
		{
			BaseObjects.Populate();

			/*

			THE ONES DONE:
			- Crate Template
			- Slime Template
			- Craft Resource Template
			- Floating Deco Template
			- Food Template
			- Liquid Template (Revisit to fix the coloring)
			- Plort Template
			- Toy Template
			- 

			TO TEST:
			- Fashion
			- Gordo
			- Animal
			
			*/

			// TEST FOR PREFABS
			
		}
	}
}
