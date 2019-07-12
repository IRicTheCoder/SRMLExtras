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
				foreach (GardenCatcher.PlantSlot slot in catcherPlantables)
				{
					___plantableDict.Add(slot.id, slot.plantedPrefab);
					___deluxeDict.Add(slot.id, slot.deluxePlantedPrefab);
				}

				/*Console.Log("Normal Plantable");
				Console.Log(string.Empty);
				PrintPlantablePrefab(___plantableDict[Identifiable.Id.CARROT_VEGGIE], string.Empty);
				Console.Log("===============================");*/

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

				if (comp is MeshFilter)
				{
					MeshFilter filter = (MeshFilter)comp;
					Console.Log(indent + $"F: Mesh: {filter.sharedMesh.name}");
				}

				if (comp is MeshRenderer)
				{
					MeshRenderer render = (MeshRenderer)comp;
					foreach (Material mat in render.sharedMaterials)
						Console.Log(indent + $"R: Material: {mat.name}");
				}

				if (comp is Rigidbody)
				{
					Rigidbody body = (Rigidbody)comp;
					foreach (PropertyInfo field in body.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
						Console.Log(indent + $"RB: {field.Name}: {field.GetValue(body, null)}");
				}

				if (comp is FixedJoint)
				{
					FixedJoint joint = (FixedJoint)comp;
					foreach (PropertyInfo field in joint.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
						Console.Log(indent + $"J: {field.Name}: {field.GetValue(joint, null)}");
				}
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
