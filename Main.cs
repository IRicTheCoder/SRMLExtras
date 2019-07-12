using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;
using SRMLExtras.Prefabs;
//using SRML.Console;

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
			Patches.ContentPatcher.Init();

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		// POST LOAD MOD
		public override void PostLoad()
		{
			Patches.ContentPatcher.prefabTest = new PlantablePrefab("patchGinger01", false, new PlantablePrefab.Spawnable(SpawnResource.Id.GINGER_PATCH, Identifiable.Id.GINGER_VEGGIE, true, null, null), false).Create() as PlantablePrefab;
		}
	}
}
