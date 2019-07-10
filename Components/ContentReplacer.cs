using System;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace SRMLExtras.Components
{
	public static class ContentReplacer
	{
		internal static Dictionary<string, Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>> purchaseUIs = new Dictionary<string, Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>>();
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
			public static void Postfix(GardenCatcher __instance, ref Dictionary<Identifiable.Id, GameObject> ___plantableDict, ref Dictionary<Identifiable.Id, GameObject> ___deluxeDict)
			{
				foreach (GardenCatcher.PlantSlot slot in catcherPlantables)
				{
					___plantableDict.Add(slot.id, slot.plantedPrefab);
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
