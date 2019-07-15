using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class VeggiePlantableTemplate : ModPrefab<VeggiePlantableTemplate>
	{
		protected readonly bool isDeluxe = false;

		protected readonly Identifiable.Id ID;
		protected readonly SpawnResource.Id resID;

		protected int minSpawn = 15;
		protected int maxSpawn = 20;
		protected float minHours = 18;
		protected float maxHours = 24;
		protected float minNutrient = 20;
		protected float waterHours = 23;

		protected int minBonusSelection = 4;
		protected float bonusChance = 0.01f;

		protected List<GameObject> toSpawn = new List<GameObject>();
		protected List<GameObject> bonusToSpawn = new List<GameObject>();

		protected SpawnResource.Id sproutID = SpawnResource.Id.CARROT_PATCH;
		protected Mesh sprout;
		protected Material[] sproutMaterials;

		protected Mesh modelMesh;
		protected Material[] modelMaterials;

		protected List<ObjectTransformValues> customSpawnJoints = null;

		public VeggiePlantableTemplate(string name, bool isDeluxe, Identifiable.Id ID, SpawnResource.Id resID, List<GameObject> toSpawn = null) : base(name)
		{
			this.isDeluxe = isDeluxe;
			this.ID = ID;
			this.resID = resID;

			if (toSpawn == null)
				this.toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			else
				this.toSpawn = toSpawn;
		}

		public VeggiePlantableTemplate SetBonusInfo(int minBonusSelection, float bonusChance = 0.01f)
		{
			this.minBonusSelection = minBonusSelection;
			this.bonusChance = bonusChance;
			return this;
		}

		public VeggiePlantableTemplate SetBonusSpawn(List<GameObject> bonusToSpawn)
		{
			this.bonusToSpawn = bonusToSpawn;
			return this;
		}

		public VeggiePlantableTemplate AddBonusSpawn(Identifiable.Id ID)
		{
			bonusToSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public VeggiePlantableTemplate AddBonusSpawn(GameObject obj)
		{
			bonusToSpawn.Add(obj);
			return this;
		}

		public VeggiePlantableTemplate SetSpawn(List<GameObject> toSpawn)
		{
			this.toSpawn = toSpawn;
			return this;
		}

		public VeggiePlantableTemplate AddSpawn(Identifiable.Id ID)
		{
			toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public VeggiePlantableTemplate AddSpawn(GameObject obj)
		{
			toSpawn.Add(obj);
			return this;
		}

		public VeggiePlantableTemplate SetCustomSprout(Mesh sprout, Material[] sproutMaterials)
		{
			this.sprout = sprout;
			this.sproutMaterials = sproutMaterials;
			return this;
		}

		public VeggiePlantableTemplate SetCustomSprout(SpawnResource.Id ID)
		{
			if (GardenObjects.modelSproutMeshs.ContainsKey(ID))
				sproutID = ID;

			return this;
		}

		public VeggiePlantableTemplate SetSpawnInfo(int minSpawn, int maxSpawn, float minHours, float maxHours, float minNutrient = 20, float waterHours = 23)
		{
			this.minSpawn = minSpawn;
			this.maxSpawn = maxSpawn;
			this.minHours = minHours;
			this.maxHours = maxHours;
			this.minNutrient = minNutrient;
			this.waterHours = waterHours;
			return this;
		}

		public VeggiePlantableTemplate SetModel(Mesh modelMesh, Material[] modelMaterials)
		{
			this.modelMesh = modelMesh;
			this.modelMaterials = modelMaterials;
			return this;
		}

		public VeggiePlantableTemplate SetSpawnJoints(List<ObjectTransformValues> spawnJoints)
		{
			if ((spawnJoints.Count < 20 && !isDeluxe) || (spawnJoints.Count < 34 && isDeluxe))
			{
				Console.LogError($"Tried to register spawn joints for '<color=white>{mainObject.Name}</color>' but they are of an invalid size (20 for normal; 34 for deluxe)");
			}

			customSpawnJoints = spawnJoints;

			return this;
		}

		public override VeggiePlantableTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new SpawnResource()
				{
					BonusChance = bonusChance,
					forceDestroyLeftoversOnSpawn = false,
					id = resID,
					MaxActiveSpawns = 0,
					MaxObjectsSpawned = maxSpawn,
					MaxSpawnIntervalGameHours = maxHours,
					MaxTotalSpawns = 0,
					minBonusSelections = minBonusSelection,
					MinNutrientObjectsSpawned = minNutrient,
					MinObjectsSpawned = minSpawn,
					MinSpawnIntervalGameHours = minHours,
					wateringDurationHours = waterHours,
					ObjectsToSpawn = toSpawn.ToArray(),
					BonusObjectsToSpawn = bonusToSpawn.ToArray()
				},
				new Create<BoxCollider>((col) =>
				{
					col.size = new Vector3(8, 0.1f, 8);
					col.center = new Vector3(0, 0, 0.1f);
					col.isTrigger = true;
				}),
				new ScaleYOnlyMarker()
				{
					doNotScaleAsReplacement = false
				}
			).AddAfterChildren(GrabJoints);

			// Add spawn joints
			for (int i = 0; i < 20; i++)
			{
				mainObject.AddChild(new GameObjectTemplate($"SpawnJoint{(i+1).ToString("00")}",
					new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.modelMeshs.ContainsKey(ID) ? GardenObjects.modelMeshs[ID] : modelMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.modelMaterials.ContainsKey(ID) ? GardenObjects.modelMaterials[ID] : modelMaterials),
					new Create<Rigidbody>((body) =>
					{
						body.drag = 0;
						body.angularDrag = 0.05f;
						body.mass = 1;
						body.useGravity = false;
						body.isKinematic = true;
					}),
					new FixedJoint(),
					new HideOnStart()
				).SetTransform(customSpawnJoints == null ? GardenObjects.spawnJoints[i] : customSpawnJoints[i])
				.SetDebugMarker(MarkerType.SpawnPoint)
				);
			}

			// Add beds
			GameObjectTemplate sprout = new GameObjectTemplate("Sprout",
				new Create<MeshFilter>((filter) => filter.sharedMesh = this.sprout ?? (GardenObjects.modelSproutMeshs[sproutID] ?? GardenObjects.modelSproutMeshs[SpawnResource.Id.CARROT_PATCH])),
				new Create<MeshRenderer>((render) => render.sharedMaterials = sproutMaterials == null ? (GardenObjects.modelSproutMaterials[sproutID] ?? GardenObjects.modelSproutMaterials[SpawnResource.Id.CARROT_PATCH]) : sproutMaterials)
			);

			GameObjectTemplate dirt = new GameObjectTemplate("Dirt",
				new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.dirtMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.dirtMaterials)
			).AddChild(new GameObjectTemplate("rocks_long",
				new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.rockMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.rockMaterials)
			)).SetTransform(Vector3.zero, Vector3.up * 90f, new Vector3(0.5f, 0.4f, 0.5f));

			GameObjectTemplate baseBeds = new GameObjectTemplate("BaseBeds");

			for (int i = 0; i < 4; i++)
			{
				(isDeluxe ? baseBeds : mainObject).AddChild(new GameObjectTemplate("Bed",
					new Create<LODGroup>((group) =>
					{
						group.localReferencePoint = Vector3.one * 0.1f;
						group.size = 8.657982f;
						group.fadeMode = LODFadeMode.None;
						group.animateCrossFading = false;
					}),
					new Create<CapsuleCollider>((col) =>
					{
						col.center = new Vector3(0, -0.6f, 0);
						col.radius = 0.8f;
						col.height = 8;
						col.direction = 2;
						col.contactOffset = 0.01f;
					})
				).SetTransform(GardenObjects.beds[i])
				.AddChild(dirt)
				.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[0]))
				.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[1]))
				.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[2]))
				.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[3]))
				);
			}

			// Add Deluxe Stuff
			if (isDeluxe)
			{
				mainObject.AddChild(baseBeds);

				// Add spawn joints
				for (int i = 20; i < 34; i++)
				{
					mainObject.AddChild(new GameObjectTemplate($"SpawnJoint{(i + 1).ToString("00")}",
						new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.modelMeshs.ContainsKey(ID) ? GardenObjects.modelMeshs[ID] : modelMesh),
						new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.modelMaterials.ContainsKey(ID) ? GardenObjects.modelMaterials[ID] : modelMaterials),
						new Create<Rigidbody>((body) =>
						{
							body.drag = 0;
							body.angularDrag = 0.05f;
							body.mass = 1;
							body.useGravity = false;
							body.isKinematic = true;
						}),
						new FixedJoint(),
						new HideOnStart()
					).SetTransform(customSpawnJoints == null ? GardenObjects.spawnJoints[i] : customSpawnJoints[i])
					.SetDebugMarker(MarkerType.SpawnPoint)
					);
				}

				// Add beds
				GameObjectTemplate dirtDeluxe = new GameObjectTemplate("Dirt",
					new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.deluxeDirtMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.deluxeDirtMaterials)
				).AddChild(new GameObjectTemplate("rocks_long",
					new Create<MeshFilter>((filter) => filter.sharedMesh = GardenObjects.deluxeRockMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = GardenObjects.deluxeRockMaterials)
				)).SetTransform(Vector3.zero, Vector3.up * 90f, new Vector3(0.5f, 0.3f, 0.5f));

				for (int i = 4; i < 6; i++)
				{
					mainObject.AddChild(new GameObjectTemplate("Bed",
						new Create<LODGroup>((group) =>
						{
							group.localReferencePoint = Vector3.one * 0.1f;
							group.size = 8.657982f;
							group.fadeMode = LODFadeMode.None;
							group.animateCrossFading = false;
						}),
						new ScaleYOnlyMarker()
						{
							doNotScaleAsReplacement = false
						}
					).SetTransform(GardenObjects.beds[i])
					.AddChild(dirtDeluxe)
					.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[4]))
					.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[5]))
					.AddChild(sprout.Clone().SetTransform(GardenObjects.bedSprouts[6]))
					);
				}
			}

			return this;
		}

		protected void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}
	}
}
