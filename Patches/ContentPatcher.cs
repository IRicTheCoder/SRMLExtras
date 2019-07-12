using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using SRML.Console;
using UnityEngine.SceneManagement;
using SRMLExtras.Prefabs;

namespace SRMLExtras.Patches
{
	public static class ContentPatcher
	{	
		internal static PlantablePrefab prefabTest;

		internal static Dictionary<string, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>> purchaseUIs = new Dictionary<string, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>>();
		internal static List<GardenCatcher.PlantSlot> catcherPlantables = new List<GardenCatcher.PlantSlot>();

		internal static Scene prefabScene = new Scene();

		public static void Init()
		{
			SceneManager.sceneLoaded += OnSceneLoad;
		}

		private static void OnSceneLoad(Scene scene, LoadSceneMode mode)
		{
			if (scene.name.Equals("worldGenerated"))
			{
				if (prefabScene.name?.Equals(string.Empty) ?? true)
					prefabScene = SceneManager.CreateScene("PrefabScene");
				else
					SceneManager.LoadSceneAsync(prefabScene.name, LoadSceneMode.Additive);

				catcherPlantables.Add(
					new GardenCatcher.PlantSlot()
					{
						id = Identifiable.Id.GINGER_VEGGIE,
						plantedPrefab = prefabTest.ToPrefabObject(),
						deluxePlantedPrefab = prefabTest.ToPrefabObject()
					}
				);
			}
		}

		// PATCHES THE GARDEN CATCHER
		[HarmonyPatch(typeof(GardenCatcher))]
		[HarmonyPatch("Awake")]
		internal static class GardenCatcherPatcher
		{
			public static void Postfix(ref Dictionary<Identifiable.Id, GameObject> ___plantableDict, ref Dictionary<Identifiable.Id, GameObject> ___deluxeDict)
			{
				GardenBedPrefab.PopulateDefaults(___plantableDict[Identifiable.Id.CARROT_VEGGIE], 
					___plantableDict[Identifiable.Id.POGO_FRUIT]);

				foreach (GardenCatcher.PlantSlot slot in catcherPlantables)
				{
					if (!___plantableDict.ContainsKey(slot.id))
						___plantableDict.Add(slot.id, slot.plantedPrefab);

					if (!___deluxeDict.ContainsKey(slot.id))
						___deluxeDict.Add(slot.id, slot.deluxePlantedPrefab);
				}
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
