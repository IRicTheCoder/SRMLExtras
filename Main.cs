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
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		// PRE LOADS MOD
		public override void PreLoad()
		{
			Console.RegisterCommand(new DebugCommand());
			//DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		// LOADS MOD
		public override void Load()
		{
			Create<Identifiable> test = new Create<Identifiable>(null);
			BoxCollider col2 = new BoxCollider();

			Console.Log("" + test);
			Console.Log("" + col2);

			BaseObjects.Populate();

			// TEST FOR PREFABS
			CrateTemplate newCrate = new CrateTemplate("crateCustom", Identifiable.Id.CRATE_DESERT_01).Create();

			/*.SetSpawnInfo(10, 15)
				.SetSpawnOptions(new List<BreakOnImpact.SpawnOption>() { new BreakOnImpact.SpawnOption()
				{
					spawn = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.INDIGONIUM_CRAFT),
					weight = 1
				}})*/
			//LookupRegistry.RegisterIdentifiablePrefab(newCrate.ToPrefab());

			PrefabUtils.DumpPrefab(newCrate.ToPrefab());
		}
	}
}
