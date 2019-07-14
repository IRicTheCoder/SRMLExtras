using System.Collections.Generic;
using SRMLExtras.Templates;
using UnityEngine;
using SRML.Console;

namespace SRMLExtras
{
	public static class BaseObjects
	{
		// DEFINED STUFF
		public readonly static ObjectTransformValues[] gardenSpawnJoints = new ObjectTransformValues[]
		{
			// NORMAL SPAWN JOINTS
			new ObjectTransformValues(new Vector3(3.40f, 0.20f, 1.26f), new Vector3(0.00f, 151.60f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.68f, 0.20f, 0.17f), new Vector3(0.00f, 316.05f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.41f, 0.20f, -0.95f), new Vector3(0.00f, 135.40f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.45f, 0.20f, -2.00f), new Vector3(0.00f, 320.33f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.60f, 0.20f, 2.52f), new Vector3(0.00f, 55.68f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(1.19f, 0.20f, 1.33f), new Vector3(0.00f, 58.41f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(1.52f, 0.20f, 0.09f), new Vector3(0.00f, 170.08f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(1.13f, 0.20f, -1.31f), new Vector3(0.00f, 135.40f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(1.30f, 0.20f, -2.75f), new Vector3(0.00f, 175.18f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(1.42f, 0.20f, 2.47f), new Vector3(0.00f, 44.95f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-0.88f, 0.20f, 2.65f), new Vector3(0.00f, 187.26f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-0.96f, 0.20f, -2.60f), new Vector3(0.00f, 320.33f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-1.13f, 0.20f, -1.33f), new Vector3(0.00f, 196.70f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-0.74f, 0.20f, 0.00f), new Vector3(0.00f, 337.57f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-1.01f, 0.20f, 1.38f), new Vector3(0.00f, 24.60f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.25f, 0.20f, 1.03f), new Vector3(0.00f, 107.21f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.07f, 0.20f, 0.11f), new Vector3(0.00f, 337.57f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.32f, 0.20f, -1.35f), new Vector3(0.00f, 135.40f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.13f, 0.20f, -2.63f), new Vector3(0.00f, 320.33f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.19f, 0.20f, 2.24f), new Vector3(0.00f, 55.68f, 0.00f), Vector3.one * 0.25f),

			// DELUXE SPAWN POINTS
			new ObjectTransformValues(new Vector3(-3.51f, 0.90f, 4.39f), new Vector3(0.00f, 100.26f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.88f, 0.90f, 3.44f), new Vector3(0.00f, 19.41f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-4.25f, 0.90f, 3.56f), new Vector3(0.00f, 177.25f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-4.46f, 0.90f, 2.99f), new Vector3(0.00f, 10.87f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-2.93f, 0.92f, 4.63f), new Vector3(0.00f, 89.06f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.80f, 0.94f, 3.89f), new Vector3(0.00f, 37.93f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(-3.31f, 0.92f, 3.99f), new Vector3(0.00f, 2.38f, 0.00f), Vector3.one * 0.25f),

			new ObjectTransformValues(new Vector3(3.51f, 0.90f, -4.39f), new Vector3(0.00f, 280.26f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.88f, 0.90f, -3.44f), new Vector3(0.00f, 199.42f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(4.25f, 0.90f, -3.56f), new Vector3(0.00f, 357.25f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(4.46f, 0.90f, -2.99f), new Vector3(0.00f, 190.87f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(2.93f, 0.92f, -4.63f), new Vector3(0.00f, 269.06f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.80f, 0.94f, -3.89f), new Vector3(0.00f, 217.93f, 0.00f), Vector3.one * 0.25f),
			new ObjectTransformValues(new Vector3(3.31f, 0.92f, -3.99f), new Vector3(0.00f, 182.38f, 0.00f), Vector3.one * 0.25f)
		};

		public readonly static ObjectTransformValues[] gardenBeds = new ObjectTransformValues[]
		{
			// NORMAL BEDS
			new ObjectTransformValues(new Vector3(-3.15f, 0.00f, 0.00f), new Vector3(0.00f, 0.00f, 0.00f), Vector3.one),
			new ObjectTransformValues(new Vector3(-1.15f, 0.00f, 0.00f), new Vector3(0.00f, 180.00f, 0.00f), Vector3.one),
			new ObjectTransformValues(new Vector3(1.24f, 0.00f, 0.00f), new Vector3(0.00f, 0.00f, 0.00f), Vector3.one),
			new ObjectTransformValues(new Vector3(3.45f, 0.00f, 0.00f), new Vector3(0.00f, 180.00f, 0.00f), Vector3.one),

			// DELUXE BEDS
			new ObjectTransformValues(new Vector3(-3.80f, 0.90f, 3.80f), new Vector3(0.00f, 45.00f, 0.00f), Vector3.one),
			new ObjectTransformValues(new Vector3(3.80f, 0.90f, -3.80f), new Vector3(0.00f, 225.00f, 0.00f), Vector3.one),
		};

		public readonly static ObjectTransformValues[] gardenBedSprouts = new ObjectTransformValues[]
		{
			// NORMAL SPROUTS
			new ObjectTransformValues(new Vector3(0.00f, 0.10f, -2.97f), new Vector3(351.24f, 0.00f, 0.00f), Vector3.one * 0.144f),
			new ObjectTransformValues(new Vector3(0.43f, 0.04f, -1.28f), new Vector3(354.85f, 6.16f, 348.38f), Vector3.one * 0.13392f),
			new ObjectTransformValues(new Vector3(-0.46f, 0.10f, 1.56f), new Vector3(347.64f, 76.44f, 357.13f), Vector3.one * 0.13392f),
			new ObjectTransformValues(new Vector3(0.33f, 0.11f, 2.42f), new Vector3(3.45f, 353.14f, 354.31f), Vector3.one * 0.1041814f),

			// DELUXE SPROUTS
			new ObjectTransformValues(new Vector3(0.00f, 0.02f, -1.23f), new Vector3(12.65f, 206.18f, 3.77f), Vector3.one * 0.144f),
			new ObjectTransformValues(new Vector3(0.27f, 0.03f, 0.10f), new Vector3(354.85f, 58.21f, 348.38f), Vector3.one * 0.1026823f),
			new ObjectTransformValues(new Vector3(0.22f, 0.03f, 1.33f), new Vector3(359.31f, 30.23f, 348.55f), Vector3.one * 0.127224f)
		};

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
			{MarkerType.SpawnPoint, new Material(fadeMat).SetInfo(new Color(1, 0, 0, 0.25f), "Spawn Point Marker")},
			{MarkerType.Plot, new Material(fadeMat).SetInfo(new Color(0, 1, 1, 0.25f), "Plot Marker")},
			{MarkerType.GadgetLocation, new Material(fadeMat).SetInfo(new Color(1, 0.5f, 0, 0.25f), "Gadget Location Marker")}
		};

		// STUFF TO BE FOUND OR CREATED
		public static Mesh cubeMesh;

		public static Mesh gardenDirtMesh;
		public static Material[] gardenDirtMaterials;

		public static Mesh gardenRockMesh;
		public static Material[] gardenRockMaterials;

		public static Mesh gardenDeluxeDirtMesh;
		public static Material[] gardenDeluxeDirtMaterials;

		public static Mesh gardenDeluxeRockMesh;
		public static Material[] gardenDeluxeRockMaterials;

		public readonly static Dictionary<Identifiable.Id, Mesh> modelMeshs = new Dictionary<Identifiable.Id, Mesh>();
		public readonly static Dictionary<Identifiable.Id, Material[]> modelMaterials = new Dictionary<Identifiable.Id, Material[]>();

		public readonly static Dictionary<SpawnResource.Id, Mesh> modelSproutMeshs = new Dictionary<SpawnResource.Id, Mesh>();
		public readonly static Dictionary<SpawnResource.Id, Material[]> modelSproutMaterials = new Dictionary<SpawnResource.Id, Material[]>();

		// The Director
		private static LookupDirector Director => GameContext.Instance.LookupDirector;

		public static void Populate()
		{
			// Dumps prefabs
			foreach (GameObject obj in Director.identifiablePrefabs)
				PrefabUtils.DumpPrefab(obj, "Identifiables");

			foreach (GameObject obj in Director.plotPrefabs)
				PrefabUtils.DumpPrefab(obj, "Plots");

			foreach (GameObject obj in Director.resourceSpawnerPrefabs)
				PrefabUtils.DumpPrefab(obj, "Resource Spawners");

			// Populate Single Objects
			GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeMesh = cubeObj.GetComponent<MeshFilter>().sharedMesh;
			Object.Destroy(cubeObj);

			GameObject carrotPatch = Director.GetResourcePrefab(SpawnResource.Id.CARROT_PATCH);
			gardenDirtMesh = carrotPatch.FindChild("Bed/Dirt").GetComponent<MeshFilter>().sharedMesh;
			gardenDirtMaterials = carrotPatch.FindChild("Bed/Dirt").GetComponent<MeshRenderer>().sharedMaterials;

			gardenRockMesh = carrotPatch.FindChild("Bed/Dirt/rocks_long").GetComponent<MeshFilter>().sharedMesh;
			gardenRockMaterials = carrotPatch.FindChild("Bed/Dirt/rocks_long").GetComponent<MeshRenderer>().sharedMaterials;

			carrotPatch = Director.GetResourcePrefab(SpawnResource.Id.CARROT_PATCH_DLX).FindChild("Bed", true);
			gardenDeluxeDirtMesh = carrotPatch.FindChild("Dirt").GetComponent<MeshFilter>().sharedMesh;
			gardenDeluxeDirtMaterials = carrotPatch.FindChild("Dirt").GetComponent<MeshRenderer>().sharedMaterials;

			gardenDeluxeRockMesh = carrotPatch.FindChild("Dirt/rocks_long").GetComponent<MeshFilter>().sharedMesh;
			gardenDeluxeRockMaterials = carrotPatch.FindChild("Dirt/rocks_long").GetComponent<MeshRenderer>().sharedMaterials;

			// Populate Lists & Dictionaries
			foreach (GameObject obj in Director.identifiablePrefabs)
			{
				GameObject modelChild = obj.FindChildWithPartialName("model_");
				if (modelChild != null)
				{
					Identifiable.Id ID = obj.GetComponent<Identifiable>().id;
					modelMeshs.Add(ID, modelChild.GetComponent<MeshFilter>().sharedMesh);
					modelMaterials.Add(ID, modelChild.GetComponent<MeshRenderer>().sharedMaterials);
				}
			}

			foreach (GameObject obj in Director.resourceSpawnerPrefabs)
			{
				foreach (GameObject child in obj.FindChildrenWithPartialName("SpawnJoint"))
					child.GetReadyForMarker(MarkerType.SpawnPoint);

				if (!obj.GetComponent<SpawnResource>()?.id.ToString().EndsWith("_DLX") ?? false)
				{
					SpawnResource.Id ID = obj.GetComponent<SpawnResource>().id;
					GameObject child = obj.FindChildWithPartialName("Sprout");

					if (child != null)
					{
						modelSproutMeshs.Add(ID, child.GetComponent<MeshFilter>().sharedMesh);
						modelSproutMaterials.Add(ID, child.GetComponent<MeshRenderer>().sharedMaterials);
					}
				}
			}

			foreach (GameObject obj in Director.plotPrefabs)
			{
				obj.GetReadyForMarker(MarkerType.Plot, 8f);
			}
		}

		public struct ObjectTransformValues
		{
			public Vector3 position;
			public Vector3 rotation;
			public Vector3 scale;

			public ObjectTransformValues(Vector3 position, Vector3 rotation, Vector3 scale)
			{
				this.position = position;
				this.rotation = rotation;
				this.scale = scale;
			}

			public override string ToString()
			{
				//return $"{position.ToString()} | {rotation.ToString()} | {scale.ToString()}";
				return $"new ObjectTransformValues(new Vector3({position.x.ToString("N2")}f, {position.y.ToString("N2")}f, {position.z.ToString("N2")}f), new Vector3({rotation.x.ToString("N2")}f, {rotation.y.ToString("N2")}f, {rotation.z.ToString("N2")}f), new Vector3({scale.x}f, {scale.y}f, {scale.z}f)),";
			}
		}
	}
}
