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

		public static Identifiable.Id CRATE_CUSTOM;
		public static Identifiable.Id ROAMING_PINK_GORDO;

		// LOADS MOD
		public override void Load()
		{
			BaseObjects.Populate();

			// TEST FOR PREFABS
			/*CrateTemplate newCrate = new CrateTemplate("crateCustom", CRATE_CUSTOM, BaseObjects.originMaterial["spicyTofu"].Group()).SetSpawnInfo(10, 15)
				.SetSpawnOptions(new List<BreakOnImpact.SpawnOption>() { new BreakOnImpact.SpawnOption()
				{
					spawn = Lookup.GetPrefab(Identifiable.Id.INDIGONIUM_CRAFT),
					weight = 1
				}}).Create();

			LookupRegistry.RegisterIdentifiablePrefab(newCrate.ToPrefab());*/

			SlimeTemplate pinkRoamGordo = Lookup.MakeRoamingGordo("roamGordoPink", ROAMING_PINK_GORDO, GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME));
			pinkRoamGordo.SetVacSize(Vacuumable.Size.LARGE);
			pinkRoamGordo.Create();

			LookupRegistry.RegisterIdentifiablePrefab(pinkRoamGordo.ToPrefab());

			Console.Log("" + pinkRoamGordo.ToPrefab().GetComponent<SlimeEat>());
			Console.Log("" + pinkRoamGordo.ToPrefab().FindChild("EatTrigger").GetComponentInParent<SlimeEat>());

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
