using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class PlantableTemplate : ModPrefab<PlantableTemplate>
	{
		private bool isDeluxe = false;
		private bool isVeggie = false;

		private Identifiable.Id ID;
		private SpawnResource.Id resID;

		private int minSpawn = 15;
		private int maxSpawn = 20;
		private float minHours = 18;
		private float maxHours = 24;
		private float minNutrient = 20;
		private float waterHours = 23;

		private int minBonusSelection = 4;
		private float bonusChance = 0.01f;

		private List<GameObject> toSpawn = new List<GameObject>();
		private List<GameObject> bonusToSpawn = new List<GameObject>();

		private SpawnResource.Id sproutId = SpawnResource.Id.CARROT_PATCH;
		private Mesh sprout;
		private Material[] sproutMaterials;

		private Mesh modelMesh;
		private Material[] modelMaterials;

		public PlantableTemplate(string name, bool isDeluxe, bool isVeggie, Identifiable.Id ID, SpawnResource.Id resID, List<GameObject> toSpawn = null) : base(name)
		{
			this.isDeluxe = isDeluxe;
			this.isVeggie = isVeggie;
			this.ID = ID;
			this.resID = resID;

			if (toSpawn == null)
				this.toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			else
				this.toSpawn = toSpawn;
		}

		public PlantableTemplate SetBonusInfo(int minBonusSelection, float bonusChance = 0.01f)
		{
			this.minBonusSelection = minBonusSelection;
			this.bonusChance = bonusChance;
			return this;
		}

		public PlantableTemplate SetBonusSpawn(List<GameObject> bonusToSpawn)
		{
			this.bonusToSpawn = bonusToSpawn;
			return this;
		}

		public PlantableTemplate AddBonusSpawn(Identifiable.Id ID)
		{
			bonusToSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public PlantableTemplate AddBonusSpawn(GameObject obj)
		{
			bonusToSpawn.Add(obj);
			return this;
		}

		public PlantableTemplate SetSpawn(List<GameObject> toSpawn)
		{
			this.toSpawn = toSpawn;
			return this;
		}

		public PlantableTemplate AddSpawn(Identifiable.Id ID)
		{
			toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public PlantableTemplate AddSpawn(GameObject obj)
		{
			toSpawn.Add(obj);
			return this;
		}

		public PlantableTemplate SetCustomSprout(Mesh sprout, Material[] sproutMaterials)
		{
			this.sprout = sprout;
			this.sproutMaterials = sproutMaterials;
			return this;
		}

		public PlantableTemplate SetCustomSprout(SpawnResource.Id ID)
		{
			this.sproutId = ID;
			return this;
		}

		public PlantableTemplate SetSpawnInfo(int minSpawn, int maxSpawn, float minHours, float maxHours, float minNutrient = 20, float waterHours = 23)
		{
			this.minSpawn = minSpawn;
			this.maxSpawn = maxSpawn;
			this.minHours = minHours;
			this.maxHours = maxHours;
			this.minNutrient = minNutrient;
			this.waterHours = waterHours;
			return this;
		}

		public PlantableTemplate SetModel(Mesh modelMesh, Material[] modelMaterials)
		{
			this.modelMesh = modelMesh;
			this.modelMaterials = modelMaterials;
			return this;
		}

		public override PlantableTemplate Create()
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
			).SetAfterChildren(GrabJoints);

			// Add spawn joints
			for (int i = 0; i < 20; i++)
			{
				mainObject.AddChild(new GameObjectTemplate($"SpawnJoint{(i+1).ToString("00")}",
					new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.modelMeshs.ContainsKey(ID) ? BaseObjects.modelMeshs[ID] : modelMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.modelMaterials.ContainsKey(ID) ? BaseObjects.modelMaterials[ID] : modelMaterials),
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
				).SetTransform(BaseObjects.gardenSpawnJoints[i])
				.SetDebugMarker(MarkerType.SpawnPoint)
				);
			}

			// Add beds
			GameObjectTemplate sprout = new GameObjectTemplate("Sprout",
				new Create<MeshFilter>((filter) => filter.sharedMesh = this.sprout ?? (BaseObjects.modelSproutMeshs[sproutId] ?? BaseObjects.modelSproutMeshs[SpawnResource.Id.CARROT_PATCH])),
				new Create<MeshRenderer>((render) => render.sharedMaterials = this.sprout == null ? (BaseObjects.modelSproutMaterials[sproutId] ?? BaseObjects.modelSproutMaterials[SpawnResource.Id.CARROT_PATCH]) : sproutMaterials)
			);

			GameObjectTemplate dirt = new GameObjectTemplate("Dirt",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.gardenDirtMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.gardenDirtMaterials)
			).AddChild(new GameObjectTemplate("rocks_long",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.gardenRockMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.gardenRockMaterials)
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
				).SetTransform(BaseObjects.gardenBeds[i])
				.AddChild(dirt)
				.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[0]))
				.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[1]))
				.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[2]))
				.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[3]))
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
						new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.modelMeshs.ContainsKey(ID) ? BaseObjects.modelMeshs[ID] : modelMesh),
						new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.modelMaterials.ContainsKey(ID) ? BaseObjects.modelMaterials[ID] : modelMaterials),
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
					).SetTransform(BaseObjects.gardenSpawnJoints[i])
					.SetDebugMarker(MarkerType.SpawnPoint)
					);
				}

				// Add beds
				GameObjectTemplate dirtDeluxe = new GameObjectTemplate("Dirt",
					new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.gardenDeluxeDirtMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.gardenDeluxeDirtMaterials)
				).AddChild(new GameObjectTemplate("rocks_long",
					new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.gardenDeluxeRockMesh),
					new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.gardenDeluxeRockMaterials)
				)).SetTransform(Vector3.zero, Vector3.up * 90f, new Vector3(0.5f, 0.3f, 0.5f));

				for (int i = 4; i < 6; i++)
				{
					(isDeluxe ? baseBeds : mainObject).AddChild(new GameObjectTemplate("Bed",
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
					).SetTransform(BaseObjects.gardenBeds[i])
					.AddChild(dirtDeluxe)
					.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[4]))
					.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[5]))
					.AddChild(sprout.SetTransform(BaseObjects.gardenBedSprouts[6]))
					);
				}
			}

			return this;
		}

		public void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}
	}
}
