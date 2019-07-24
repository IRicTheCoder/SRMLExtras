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

		// PRE LOADS MOD
		public override void PreLoad()
		{
			Console.RegisterCommand(new DebugCommand());
			//DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		public static Identifiable.Id CRATE_CUSTOM;

		// LOADS MOD
		public override void Load()
		{
			BaseObjects.Populate();

			// TEST FOR PREFABS
			CrateTemplate newCrate = new CrateTemplate("crateCustom", CRATE_CUSTOM, BaseObjects.originMaterial["spicyTofu"].Group()).SetSpawnInfo(10, 15)
				.SetSpawnOptions(new List<BreakOnImpact.SpawnOption>() { new BreakOnImpact.SpawnOption()
				{
					spawn = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.INDIGONIUM_CRAFT),
					weight = 1
				}}).Create();

			LookupRegistry.RegisterIdentifiablePrefab(newCrate.ToPrefab());

			PrefabUtils.DumpPrefab(GameContext.Instance.LookupDirector.GetGordo(Identifiable.Id.PINK_GORDO));
		}
	}
}
