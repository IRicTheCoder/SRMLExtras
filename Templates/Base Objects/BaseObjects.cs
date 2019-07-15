using System.Collections.Generic;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SRMLExtras.Templates
{
	public static class BaseObjects
	{
		// The Director
		private static LookupDirector Director => GameContext.Instance.LookupDirector;

		// Markers
		public readonly static Material fadeMat = new Material(Shader.Find("Standard")).Initialize((mat) =>
		{
			mat.SetFloat("_Mode", 2);
			mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			mat.SetInt("_ZWrite", 0);
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.EnableKeyword("_ALPHABLEND_ON");
			mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			mat.renderQueue = 3000;
		});

		public readonly static Dictionary<MarkerType, Material> markerMaterials = new Dictionary<MarkerType, Material>()
		{
			{MarkerType.SpawnPoint, new Material(fadeMat).SetInfo(new Color(1, 0, 0, 0.75f), "Spawn Point Marker")},
			{MarkerType.Plot, new Material(fadeMat).SetInfo(new Color(0, 1, 1, 0.75f), "Plot Marker")},
			{MarkerType.GadgetLocation, new Material(fadeMat).SetInfo(new Color(1, 0.5f, 0, 0.75f), "Gadget Location Marker")},
			{MarkerType.DroneNode, new Material(fadeMat).SetInfo(new Color(1, 0, 0.5f, 0.75f), "Drone Node Marker")}
		};

		public readonly static Dictionary<MarkerType, Material> markerAreaMaterials = new Dictionary<MarkerType, Material>()
		{
			{MarkerType.Plot, new Material(fadeMat).SetInfo(new Color(0, 1, 1, 0.2f), "Plot Area")},
			{MarkerType.GadgetLocation, new Material(fadeMat).SetInfo(new Color(1, 0.5f, 0, 0.2f), "Gadget Location Area")},
		};

		public static Mesh cubeMesh;

		public static GameObject markerIdentifier;

		// Populates required values
		internal static void Populate()
		{
			// Dumps prefabs
			foreach (GameObject obj in Director.identifiablePrefabs)
				PrefabUtils.DumpPrefab(obj, "Identifiables");

			foreach (GameObject obj in Director.plotPrefabs)
				PrefabUtils.DumpPrefab(obj, "Plots");

			foreach (GameObject obj in Director.resourceSpawnerPrefabs)
				PrefabUtils.DumpPrefab(obj, "Resource Spawners");

			// Gets the cube for the markers
			GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeMesh = cubeObj.GetComponent<MeshFilter>().sharedMesh;
			Object.Destroy(cubeObj);

			markerIdentifier = new GameObject("MarkerIdentifier", typeof(MarkerRaycast));
			Object.DontDestroyOnLoad(markerIdentifier);

			// Adds markers to objects
			foreach (GameObject obj in Director.plotPrefabs)
			{
				obj.GetReadyForMarker(MarkerType.Plot, 8f);
			}

			foreach (GameObject obj in Director.resourceSpawnerPrefabs)
			{
				foreach (GameObject child in obj.FindChildrenWithPartialName("SpawnJoint"))
					child.GetReadyForMarker(MarkerType.SpawnPoint);

				foreach (GameObject child in obj.FindChildrenWithPartialName("NodeLoc"))
					child.GetReadyForMarker(MarkerType.DroneNode, 3f);
			}

			Console.Log(SceneContext.Instance.GameModel.AllGadgetSites().Count.ToString());

			// Populates all other object classes
			GardenObjects.Populate();

			// Adds Late Populate method
			SceneManager.sceneLoaded += LatePopulate;
		}

		internal static void LatePopulate(Scene sceneLoaded, LoadSceneMode mode)
		{
			if (sceneLoaded.name.Equals("worldGenerated"))
			{
				foreach (GadgetSiteModel obj in SceneContext.Instance.GameModel.AllGadgetSites().Values)
				{
					obj.transform.gameObject.GetReadyForMarker(MarkerType.GadgetLocation, 4f);
				}

				/*System.Type regionSet = typeof(RegionRegistry.RegionSetId);
				foreach (string id in System.Enum.GetNames(regionSet))
				{
					foreach (Region obj in SceneContext.Instance.RegionRegistry.GetRegions((RegionRegistry.RegionSetId)System.Enum.Parse(regionSet, id)))
					{
						obj.transform.gameObject.GetReadyForMarker(MarkerType.Region);
					}
				}*/
			}
		}
	}
}
