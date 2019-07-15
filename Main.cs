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
	[EnumHolder]
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		// PRE LOAD MOD
		public override void PreLoad()
		{
			Console.RegisterCommand(new DebugCommand());
			DebugCommand.DebugMode = true;

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		public static SpawnResource.Id KOOKADOBA_BUSH;
		public static SpawnResource.Id KOOKADOBA_BUSH_DLX;

		// POST LOAD MOD
		public override void PostLoad()
		{
			BaseObjects.Populate();

			GameObject gingerSpawnResource = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.GINGER_VEGGIE).CreatePrefabCopy();
			gingerSpawnResource.GetComponent<ResourceCycle>().unripeGameHours = 12;

			GameObject kookadobaSpawnResource = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.KOOKADOBA_FRUIT).CreatePrefabCopy();
			kookadobaSpawnResource.GetComponent<ResourceCycle>().unripeGameHours = 6;

			VeggiePlantableTemplate prefabTest = new VeggiePlantableTemplate("patchGinger01", false, Identifiable.Id.PARSNIP_VEGGIE, SpawnResource.Id.GINGER_PATCH)
				.SetSpawnInfo(10, 20, 18, 24).AddBonusSpawn(gingerSpawnResource).SetBonusInfo(2, 0.1f).SetCustomSprout(SpawnResource.Id.PARSNIP_PATCH).Create();

			VeggiePlantableTemplate prefabTest2 = new VeggiePlantableTemplate("patchGinger04", true, Identifiable.Id.PARSNIP_VEGGIE, SpawnResource.Id.GINGER_PATCH_DLX)
				.SetSpawnInfo(12, 24, 18, 24).AddBonusSpawn(gingerSpawnResource).SetBonusInfo(2, 0.1f).SetCustomSprout(SpawnResource.Id.PARSNIP_PATCH).Create();

			BushPlantableTemplate prefabTest3 = new BushPlantableTemplate("bushKookadoba01", false, Identifiable.Id.KOOKADOBA_FRUIT, KOOKADOBA_BUSH, new List<GameObject>() { kookadobaSpawnResource })
				.SetSpawnInfo(10, 20, 18, 24).SetCustomTree(SpawnResource.Id.PEAR_TREE).SetCustomLeaves(SpawnResource.Id.POGO_TREE).Create();

			BushPlantableTemplate prefabTest4 = new BushPlantableTemplate("bushKookadoba04", true, Identifiable.Id.KOOKADOBA_FRUIT, KOOKADOBA_BUSH_DLX, new List<GameObject>() { kookadobaSpawnResource })
				.SetSpawnInfo(12, 24, 18, 24).SetCustomTree(SpawnResource.Id.PEAR_TREE).SetCustomLeaves(SpawnResource.Id.POGO_TREE).Create();

			Patches.ContentPatcher.catcherPlantables.Add(
					new GardenCatcher.PlantSlot()
					{
						id = Identifiable.Id.GINGER_VEGGIE,
						plantedPrefab = prefabTest.ToPrefab(),
						deluxePlantedPrefab = prefabTest2.ToPrefab()
					}
				);

			Patches.ContentPatcher.catcherPlantables.Add(
					new GardenCatcher.PlantSlot()
					{
						id = Identifiable.Id.KOOKADOBA_FRUIT,
						plantedPrefab = prefabTest3.ToPrefab(),
						deluxePlantedPrefab = prefabTest4.ToPrefab()
					}
				);

			// TODO: For SRML 1.8
			try
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
			}
		}
	}
}
