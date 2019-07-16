using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;
using SRMLExtras.Templates;
using SRML.Console;
using SRML.Utils;
using UnityEngine;
using SRML.Utils.Enum;

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
			Console.RegisterCommand(new DebugCommand());
			//DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		// POST LOAD MOD
		public override void PostLoad()
		{
			BaseObjects.Populate();

			// TODO: For SRML 1.8
			/*try
			{
				GameContext.Instance.LookupDirector.GetResourcePrefab(SpawnResource.Id.GINGER_PATCH);
			}
			catch (KeyNotFoundException)
			{
				Dictionary<SpawnResource.Id, GameObject> resourcePrefabDict = typeof(LookupDirector)
					.GetField("resourcePrefabDict", BindingFlags.NonPublic | BindingFlags.Instance)
					.GetValue(GameContext.Instance.LookupDirector) as Dictionary<SpawnResource.Id, GameObject>;

				resourcePrefabDict.Add(SpawnResource.Id.GINGER_PATCH_DLX, prefabTest2.ToPrefab());
				resourcePrefabDict.Add(SpawnResource.Id.GINGER_PATCH, prefabTest.ToPrefab());

				resourcePrefabDict.Add(KOOKADOBA_BUSH_DLX, prefabTest4.ToPrefab());
				resourcePrefabDict.Add(KOOKADOBA_BUSH, prefabTest3.ToPrefab());
			}*/
		}
	}
}
