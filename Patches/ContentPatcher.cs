using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using SRML.Console;
using UnityEngine.SceneManagement;

namespace SRMLExtras.Patches
{
	public static class ContentPatcher
	{
		internal static Scene prefabScene = SceneManager.CreateScene("PrefabScene");

		internal static Dictionary<string, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>> purchaseUIs = new Dictionary<string, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>>();
		internal static List<GardenCatcher.PlantSlot> catcherPlantables = new List<GardenCatcher.PlantSlot>()
		{
			/*new GardenCatcher.PlantSlot()
			{
				id = Identifiable.Id.KOOKADOBA_FRUIT,
				plantedPrefab = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.KOOKADOBA_FRUIT),
				deluxePlantedPrefab = GameContext.Instance.LookupDirector.GetPrefab(Identifiable.Id.KOOKADOBA_FRUIT)
			}*/
		};

		// PATCHES THE GARDEN CATCHER
		[HarmonyPatch(typeof(GardenCatcher))]
		[HarmonyPatch("Awake")]
		internal static class GardenCatcherPatcher
		{
			public static void Postfix(ref Dictionary<Identifiable.Id, GameObject> ___plantableDict, ref Dictionary<Identifiable.Id, GameObject> ___deluxeDict)
			{
				foreach (GameObject obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
					Console.Log(obj.name);

				/*foreach (GardenCatcher.PlantSlot slot in catcherPlantables)
				{
					___plantableDict.Add(slot.id, slot.plantedPrefab);
					___deluxeDict.Add(slot.id, slot.deluxePlantedPrefab);
				}*/
				Console.Log("Normal Plantable");
				//foreach (GameObject go in ___plantableDict.Values)
				//{
				Console.Log(string.Empty);
				PrintPlantablePrefab(___plantableDict[Identifiable.Id.CARROT_VEGGIE], string.Empty);
				Console.Log("===============================");
				//}

				//Console.Log("Deluxe Plantable");
			}
		}

		private static void PrintPlantablePrefab(GameObject obj, string indent)
		{
			Console.Log(indent + obj.name);
			Console.Log(indent + "T: " + obj.tag);
			Console.Log(indent + "L: " + LayerMask.LayerToName(obj.layer));

			foreach (Component comp in obj.GetComponents<Component>())
			{
				Console.Log(indent + "C: " + comp.GetType().Name);

				if (comp is Transform)
				{
					Transform trans = (Transform)comp;
					Console.Log(indent + $"TR: Position: {trans.localPosition}");
					Console.Log(indent + $"TR: Rotation: {trans.localEulerAngles}");
					Console.Log(indent + $"TR: Scale: {trans.localScale}");
				}

				/*if (comp is SpawnResource)
				{
					SpawnResource res = (SpawnResource)comp;
					foreach (GameObject go in res.ObjectsToSpawn)
						PrintPlantablePrefab(go, "S: ");

					Console.Log(string.Empty);

					foreach (GameObject go in res.BonusObjectsToSpawn)
						PrintPlantablePrefab(go, "S: ");

					Console.Log(string.Empty);
					Console.Log($"S: BonusChance: {res.BonusChance}");
					Console.Log($"S: forceDestroyLeftoversOnSpawn: {res.forceDestroyLeftoversOnSpawn}");
					Console.Log($"S: id: {res.id}");
					Console.Log($"S: MaxActiveSpawns: {res.MaxActiveSpawns}");
					Console.Log($"S: MaxObjectsSpawned: {res.MaxObjectsSpawned}");
					Console.Log($"S: MaxSpawnIntervalGameHours: {res.MaxSpawnIntervalGameHours}");
					Console.Log($"S: MaxTotalSpawns: {res.MaxTotalSpawns}");
					Console.Log($"S: minBonusSelections: {res.minBonusSelections}");
					Console.Log($"S: MinNutrientObjectsSpawned: {res.MinNutrientObjectsSpawned}");
					Console.Log($"S: MinObjectsSpawned: {res.MinObjectsSpawned}");
					Console.Log($"S: MinSpawnIntervalGameHours: {res.MinSpawnIntervalGameHours}");
					Console.Log($"S: wateringDurationHours: {res.wateringDurationHours}");
					Console.Log(string.Empty);
				}*/

				/*if (comp is ScaleYOnlyMarker)
				{
					ScaleYOnlyMarker scale = (ScaleYOnlyMarker)comp;
					Console.Log($"Y: {scale.doNotScaleAsReplacement}");
				}*/

				/*if (comp is BoxCollider)
				{
					BoxCollider col = (BoxCollider)comp;
					Console.Log($"CO: Size: {col.size}");
					Console.Log($"CO: Center: {col.center}");
					Console.Log($"CO: Trigger: {col.isTrigger}");
					Console.Log($"CO: Bounds: {col.bounds}");
				}*/
			}

			foreach (Transform child in obj.transform)
			{
				if (child.name.Contains("SpawnJoint"))
					PrintPlantablePrefab(child.gameObject, "  " + indent);
			}
		}

		// PATCHES PURCHASE UIs
		[HarmonyPatch(typeof(UITemplates))]
		[HarmonyPatch("CreatePurchaseUI")]
		internal static class UITemplatesPatcher
		{
			public static void Prefix(UITemplates __instance, string titleKey, ref PurchaseUI.Purchasable[] purchasables)
			{
				if (purchaseUIs.ContainsKey(titleKey))
				{
					purchasables = purchaseUIs[titleKey].Invoke(purchasables);
				}
			}
		}
	}
}
