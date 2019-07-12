using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// A modded version of a plantable prefab
	/// </summary>
	public class PlantablePrefab : ModdedPrefab
	{
		private string patchName = "patchModded01";
		private bool isDeluxe = false;
		private Spawnable spawnable;

		public PlantablePrefab(string patchName, bool isDeluxe, Spawnable spawnable, bool intern) : base(patchName, intern)
		{
			this.patchName = patchName;
			this.isDeluxe = isDeluxe;
			this.spawnable = spawnable;

			// TODO: For SRML 1.8
			try
			{
				GameContext.Instance.LookupDirector.GetResourcePrefab(spawnable.ID);
			}
			catch (KeyNotFoundException)
			{
				Dictionary<SpawnResource.Id, GameObject> resourcePrefabDict = typeof(LookupDirector)
					.GetField("resourcePrefabDict", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
					.GetValue(GameContext.Instance.LookupDirector) as Dictionary<SpawnResource.Id, GameObject>;

				resourcePrefabDict.Add(spawnable.ID, ToPrefabObject());
			}
		}

		public override ModdedPrefab Create()
		{
			// Create the main object
			MainObject = new ModdedGameObject(patchName, typeof(SpawnResource),
				typeof(BoxCollider), typeof(ScaleYOnlyMarker));

			// Create the spawn joints
			SpawnJointPrefab[] joints = new SpawnJointPrefab[20];

			joints[0] = new SpawnJointPrefab("SpawnJoint01", true, new Vector3(3.4f, 0.2f, 1.3f), new Vector3(0, 151.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[1] = new SpawnJointPrefab("SpawnJoint02", true, new Vector3(3.7f, 0.2f, 0.2f), new Vector3(0, 316f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[2] = new SpawnJointPrefab("SpawnJoint03", true, new Vector3(3.4f, 0.2f, -1.3f), new Vector3(0, 134.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[3] = new SpawnJointPrefab("SpawnJoint04", true, new Vector3(3.4f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[4] = new SpawnJointPrefab("SpawnJoint05", true, new Vector3(3.6f, 0.2f, 2.5f), new Vector3(0, 55.7f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[5] = new SpawnJointPrefab("SpawnJoint06", true, new Vector3(1.2f, 0.2f, 1.3f), new Vector3(0, 58.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[6] = new SpawnJointPrefab("SpawnJoint07", true, new Vector3(1.5f, 0.2f, 0.1f), new Vector3(0, 170.1f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[7] = new SpawnJointPrefab("SpawnJoint08", true, new Vector3(1.1f, 0.2f, -1.3f), new Vector3(0, 135.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[8] = new SpawnJointPrefab("SpawnJoint09", true, new Vector3(1.3f, 0.2f, -2.8f), new Vector3(0, 175.2f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[9] = new SpawnJointPrefab("SpawnJoint10", true, new Vector3(1.4f, 0.2f, 2.5f), new Vector3(0, 44.9f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[10] = new SpawnJointPrefab("SpawnJoint11", true, new Vector3(-0.9f, 0.2f, 2.7f), new Vector3(0, 187.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[11] = new SpawnJointPrefab("SpawnJoint12", true, new Vector3(-1f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[12] = new SpawnJointPrefab("SpawnJoint13", true, new Vector3(-1.1f, 0.2f, -1.3f), new Vector3(0, 196.7f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[13] = new SpawnJointPrefab("SpawnJoint14", true, new Vector3(-0.7f, 0.2f, 0f), new Vector3(0, 337.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[14] = new SpawnJointPrefab("SpawnJoint15", true, new Vector3(-1f, 0.2f, 1.4f), new Vector3(0, 24.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[15] = new SpawnJointPrefab("SpawnJoint16", true, new Vector3(-3.3f, 0.2f, 1.5f), new Vector3(0, 107.2f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[16] = new SpawnJointPrefab("SpawnJoint17", true, new Vector3(-3.1f, 0.2f, 0.1f), new Vector3(0, 337.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[17] = new SpawnJointPrefab("SpawnJoint18", true, new Vector3(-3.3f, 0.2f, -1.4f), new Vector3(0, 135.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[18] = new SpawnJointPrefab("SpawnJoint19", true, new Vector3(-3.1f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[19] = new SpawnJointPrefab("SpawnJoint20", true, new Vector3(-3.2f, 0.2f, 2.7f), new Vector3(0, 55.7f, 0), Vector3.one * 0.3f, spawnable.identID);

			foreach (SpawnJointPrefab joint in joints)
				MainObject.AddChild(joint);

			// Create the beds
			/*GardenBedPrefab[] beds = new GardenBedPrefab[4];

			beds[0] = new GardenBedPrefab("Bed", new Vector3(-3.2f, 0, 0), spawnable.veggie, spawnable.sprout, spawnable.sproutMaterials, true);
			beds[1] = new GardenBedPrefab("Bed", new Vector3(-1.2f, 0, 0), spawnable.veggie, spawnable.sprout, spawnable.sproutMaterials, true);
			beds[2] = new GardenBedPrefab("Bed", new Vector3(1.2f, 0, 0), spawnable.veggie, spawnable.sprout, spawnable.sproutMaterials, true);
			beds[3] = new GardenBedPrefab("Bed", new Vector3(3.5f, 0, 0), spawnable.veggie, spawnable.sprout, spawnable.sproutMaterials, true);

			foreach (GardenBedPrefab bed in beds)
				MainObject.AddChild(bed);*/

			return this;
		}

		public override void Setup(GameObject root)
		{
			// Setup for Main Object
			SpawnResource res = root.GetComponent<SpawnResource>();
			res.BonusChance = 0.01f;
			res.forceDestroyLeftoversOnSpawn = false;
			res.id = spawnable.ID;
			res.MaxActiveSpawns = 0;
			res.MaxObjectsSpawned = 20;
			res.MaxSpawnIntervalGameHours = 24;
			res.MaxTotalSpawns = 0;
			res.minBonusSelections = 0;
			res.MinNutrientObjectsSpawned = 20;
			res.MinObjectsSpawned = 15;
			res.MinSpawnIntervalGameHours = 18;
			res.wateringDurationHours = 23;
			res.ObjectsToSpawn = spawnable.objectsToSpawn.ToArray();
			res.BonusObjectsToSpawn = spawnable.bonusObjectsToSpawn.ToArray();

			foreach (GameObject obj in res.ObjectsToSpawn)
			{
				if (obj.GetComponent<ResourceCycle>().unripeGameHours == 0)
					obj.GetComponent<ResourceCycle>().unripeGameHours = 6;
			}

			foreach (GameObject obj in res.BonusObjectsToSpawn)
			{
				if (obj.GetComponent<ResourceCycle>().unripeGameHours == 0)
					obj.GetComponent<ResourceCycle>().unripeGameHours = 6;
			}

			BoxCollider box = root.GetComponent<BoxCollider>();
			box.size = new Vector3(8f, 0.1f, 8f);
			box.center = new Vector3(0, 0, 0.1f);
			box.isTrigger = true;

			root.GetComponent<ScaleYOnlyMarker>().doNotScaleAsReplacement = false;

			// Setup Joints
			res.SpawnJoints = root.GetComponentsInChildren<FixedJoint>();
		}

		public class Spawnable
		{
			public readonly Identifiable.Id identID;
			public readonly SpawnResource.Id ID;

			public readonly bool veggie;

			public readonly List<GameObject> objectsToSpawn = new List<GameObject>();
			public readonly List<GameObject> bonusObjectsToSpawn = new List<GameObject>();

			public readonly Mesh sprout;
			public readonly Material[] sproutMaterials;

			public Spawnable(SpawnResource.Id ID, Identifiable.Id identID, bool veggie, GameObject[] objectsToSpawn, GameObject[] bonusObjectsToSpawn)
			{
				this.ID = ID;
				this.identID = identID;
				this.veggie = veggie;

				if (objectsToSpawn != null)
					this.objectsToSpawn.AddRange(objectsToSpawn);
				else
					this.objectsToSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(identID));

				if (bonusObjectsToSpawn != null)
					this.bonusObjectsToSpawn.AddRange(bonusObjectsToSpawn);

				sprout = null;
				sproutMaterials = null;
			}

			public Spawnable(SpawnResource.Id ID, Identifiable.Id identID, bool veggie, GameObject[] objectsToSpawn, GameObject[] bonusObjectsToSpawn, Mesh sprout, Material[] sproutMaterials)
			{
				this.ID = ID;
				this.identID = identID;
				this.veggie = veggie;

				if (objectsToSpawn != null)
					this.objectsToSpawn.AddRange(objectsToSpawn);
				else
					this.objectsToSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(identID));

				if (bonusObjectsToSpawn != null)
					this.bonusObjectsToSpawn.AddRange(bonusObjectsToSpawn);

				this.sprout = sprout;
				this.sproutMaterials = sproutMaterials;
			}
		}
	}
}
