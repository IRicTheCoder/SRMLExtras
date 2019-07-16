using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using SRML.Console;

namespace SRMLExtras.Registries
{
	public static class ContentPatcher
	{	
		internal static Dictionary<System.Type, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>> purchaseUIs = new Dictionary<System.Type, System.Func<PurchaseUI.Purchasable[], PurchaseUI.Purchasable[]>>();
		internal static List<GardenCatcher.PlantSlot> catcherPlantables = new List<GardenCatcher.PlantSlot>();

		// PATCHES THE GARDEN CATCHER
		[HarmonyPatch(typeof(GardenCatcher))]
		[HarmonyPatch("Awake")]
		internal static class GardenCatcherPatcher
		{
			public static void Postfix(ref Dictionary<Identifiable.Id, GameObject> ___plantableDict, ref Dictionary<Identifiable.Id, GameObject> ___deluxeDict)
			{
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
			public static void Prefix(UITemplates __instance, ref PurchaseUI.Purchasable[] purchasables, PurchaseUI.OnClose onClose)
			{
				if (purchaseUIs.ContainsKey(onClose.Target.GetType()))
				{
					purchasables = purchaseUIs[onClose.Method.ReflectedType].Invoke(purchasables);
				}
			}
		}
	}
}
