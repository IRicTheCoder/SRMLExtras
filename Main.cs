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
			Console.RegisterCommand(new DebugCommand());
			//DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		public static Identifiable.Id TOFU_FRUIT;

		public static Identifiable.Id TOFU_VEGGIE;

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
			
			*/

			// TEST FOR PREFABS


			// DUMP PREFABS
			/*PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.TELEPORTER_GOLD).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.BLUE_CORAL_COLUMNS).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.DRONE).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.DRONE_ADVANCED).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.EXTRACTOR_DRILL_TITAN).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.EXTRACTOR_APIARY_ROYAL).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.EXTRACTOR_PUMP_ABYSSAL).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.FASHION_POD_CUTE).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.FASHION_POD_REMOVER).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.GORDO_SNARE_MASTER).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.MARKET_LINK).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.REFINERY_LINK).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.WARP_DEPOT_PINK).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.HYDRO_SHOWER).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.HYDRO_TURRET).prefab);
			PrefabUtils.DumpPrefab(Lookup.GetGadgetEntry(Gadget.Id.SUPER_HYDRO_TURRET).prefab);*/
		}
	}
}
