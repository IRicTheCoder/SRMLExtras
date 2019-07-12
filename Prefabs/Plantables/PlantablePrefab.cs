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

		/*
		THINGS TO LOOK AT:
		- LookupDirector GetResourcePrefab
		
		THIS IS THE BASE OF THE PLANTABLE PREFAB
		patchCarrot01
		C: Transform
		C: SpawnResource
		C: BoxCollider
		C: ScaleYOnlyMarker

		THEN 20 OF THIS AS CHILDREN (more in case of deluxe)
		SpawnJoint01
		C: Transform
		C: MeshFilter
		C: MeshRenderer
		C: Rigidbody
		C: FixedJoint
		C: HideOnStart

		THIS IS THE SPAWN RESOURCE PREFAB (Make it's own prefab)
		S:veggieCarrot
		S:C: Transform
		S:C: MeshFilter
		S:C: CapsuleCollider
		S:C: Rigidbody
		S:C: Vacuumable
		S:C: Identifiable
		S:C: ResourceCycle
		S:C: SECTR_PointSource
		S:C: BuoyancyOffset
		S:C: DragFloatReactor
		S:C: PlaySoundOnHit
		S:C: CollisionAggregator
		S:C: SphereCollider
		S:C: RegionMember
		  S:DelaunchTrigger
		  S:C: Transform
		  S:C: SphereCollider
		  S:C: VacDelaunchTrigger
		  S:model_carrot
		  S:C: Transform
		  S:C: MeshRenderer
		  S:C: MeshFilter
		  S:Shadow
		  S:C: Transform
		  S:C: MeshFilter
		  S:C: MeshRenderer
		*/

		public PlantablePrefab(string patchName, bool isDeluxe, Spawnable spawnable) : base(patchName)
		{
			this.patchName = patchName;
			this.isDeluxe = isDeluxe;
			this.spawnable = spawnable;
		}

		public override ModdedPrefab Create()
		{
			// Create the main object
			MainObject = new ModdedGameObject(patchName, typeof(SpawnResource),
				typeof(BoxCollider), typeof(ScaleYOnlyMarker));

			// Create the spawn joints
			SpawnJointPrefab[] joints = new SpawnJointPrefab[20];

			joints[0] = new SpawnJointPrefab("SpawnJoint01", new Vector3(3.4f, 0.2f, 1.3f), new Vector3(0, 151.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[1] = new SpawnJointPrefab("SpawnJoint02", new Vector3(3.7f, 0.2f, 0.2f), new Vector3(0, 316f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[2] = new SpawnJointPrefab("SpawnJoint03", new Vector3(3.4f, 0.2f, -1.3f), new Vector3(0, 134.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[3] = new SpawnJointPrefab("SpawnJoint04", new Vector3(3.4f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[4] = new SpawnJointPrefab("SpawnJoint05", new Vector3(3.6f, 0.2f, 2.5f), new Vector3(0, 55.7f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[5] = new SpawnJointPrefab("SpawnJoint06", new Vector3(1.2f, 0.2f, 1.3f), new Vector3(0, 58.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[6] = new SpawnJointPrefab("SpawnJoint07", new Vector3(1.5f, 0.2f, 0.1f), new Vector3(0, 170.1f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[7] = new SpawnJointPrefab("SpawnJoint08", new Vector3(1.1f, 0.2f, -1.3f), new Vector3(0, 135.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[8] = new SpawnJointPrefab("SpawnJoint09", new Vector3(1.3f, 0.2f, -2.8f), new Vector3(0, 175.2f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[9] = new SpawnJointPrefab("SpawnJoint10", new Vector3(1.4f, 0.2f, 2.5f), new Vector3(0, 44.9f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[10] = new SpawnJointPrefab("SpawnJoint11", new Vector3(-0.9f, 0.2f, 2.7f), new Vector3(0, 187.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[11] = new SpawnJointPrefab("SpawnJoint12", new Vector3(-1f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[12] = new SpawnJointPrefab("SpawnJoint13", new Vector3(-1.1f, 0.2f, -1.3f), new Vector3(0, 196.7f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[13] = new SpawnJointPrefab("SpawnJoint14", new Vector3(-0.7f, 0.2f, 0f), new Vector3(0, 337.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[14] = new SpawnJointPrefab("SpawnJoint15", new Vector3(-1f, 0.2f, 1.4f), new Vector3(0, 24.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[15] = new SpawnJointPrefab("SpawnJoint16", new Vector3(-3.3f, 0.2f, 1.5f), new Vector3(0, 107.2f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[16] = new SpawnJointPrefab("SpawnJoint17", new Vector3(-3.1f, 0.2f, 0.1f), new Vector3(0, 337.6f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[17] = new SpawnJointPrefab("SpawnJoint18", new Vector3(-3.3f, 0.2f, -1.4f), new Vector3(0, 135.4f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[18] = new SpawnJointPrefab("SpawnJoint19", new Vector3(-3.1f, 0.2f, -2.6f), new Vector3(0, 320.3f, 0), Vector3.one * 0.3f, spawnable.identID);
			joints[19] = new SpawnJointPrefab("SpawnJoint20", new Vector3(-3.2f, 0.2f, 2.7f), new Vector3(0, 55.7f, 0), Vector3.one * 0.3f, spawnable.identID);

			foreach (SpawnJointPrefab joint in joints)
				MainObject.AddChild(joint);

			// Create the beds

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
			public readonly List<GameObject> objectsToSpawn = new List<GameObject>();
			public readonly List<GameObject> bonusObjectsToSpawn = new List<GameObject>();

			public Spawnable(SpawnResource.Id ID, Identifiable.Id identID, GameObject[] objectsToSpawn, GameObject[] bonusObjectsToSpawn)
			{
				this.ID = ID;
				this.identID = identID;

				if (objectsToSpawn != null)
					this.objectsToSpawn.AddRange(objectsToSpawn);
				else
					this.objectsToSpawn.Add(GameContext.Instance.LookupDirector.GetResourcePrefab(ID));

				if (bonusObjectsToSpawn != null)
					this.bonusObjectsToSpawn.AddRange(bonusObjectsToSpawn);
			}
		}
	}
}
