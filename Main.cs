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

		[EnumHolder]
		public SpawnResource.Id PINK_SLIME_PATCH;

		// POST LOAD MOD
		public override void PostLoad()
		{
			BaseObjects.Populate();

			GameObject gingerSpawnResource = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.GINGER_VEGGIE).CreatePrefabCopy();
			gingerSpawnResource.GetComponent<ResourceCycle>().unripeGameHours = 12;

			PlantableTemplate prefabTest = new PlantableTemplate("patchGinger01", false, true, Identifiable.Id.PARSNIP_VEGGIE, SpawnResource.Id.GINGER_PATCH)
				.SetSpawnInfo(10, 20, 18, 24).AddBonusSpawn(gingerSpawnResource).SetBonusInfo(2, 0.1f).SetCustomSprout(SpawnResource.Id.PARSNIP_PATCH).Create();

			PlantableTemplate prefabTest2 = new PlantableTemplate("patchGinger04", true, true, Identifiable.Id.PARSNIP_VEGGIE, SpawnResource.Id.GINGER_PATCH_DLX)
				.SetSpawnInfo(10, 20, 18, 24).AddBonusSpawn(gingerSpawnResource).SetBonusInfo(2, 0.1f).SetCustomSprout(SpawnResource.Id.PARSNIP_PATCH).Create();

			Patches.ContentPatcher.catcherPlantables.Add(
					new GardenCatcher.PlantSlot()
					{
						id = Identifiable.Id.GINGER_VEGGIE,
						plantedPrefab = prefabTest.ToPrefab(),
						deluxePlantedPrefab = prefabTest2.ToPrefab()
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
			}
		}
	}
}
