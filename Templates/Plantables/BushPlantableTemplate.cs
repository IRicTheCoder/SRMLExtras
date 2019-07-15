using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class BushPlantableTemplate : ModPrefab<BushPlantableTemplate>
	{
		private readonly bool isDeluxe = false;

		private readonly Identifiable.Id ID;
		private readonly SpawnResource.Id resID;

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

		private SpawnResource.Id treeID = SpawnResource.Id.POGO_TREE;
		private Mesh treeCol;
		private Mesh tree;
		private Material[] treeMaterials;

		private SpawnResource.Id leavesID = SpawnResource.Id.POGO_TREE;
		private Mesh leavesCol;
		private Mesh leaves;
		private Material[] leavesMaterials;

		private bool customLeavePos = false;

		private Vector3 leavesPosition = new Vector3(0, 3.2f, 0);
		private Vector3 leavesDeluxePosition = new Vector3(0, 3.1f, 0);

		private Mesh modelMesh;
		private Material[] modelMaterials;

		private List<ObjectTransformValues> customSpawnJoints = null;

		public BushPlantableTemplate(string name, bool isDeluxe, Identifiable.Id ID, SpawnResource.Id resID, List<GameObject> toSpawn = null) : base(name)
		{
			this.isDeluxe = isDeluxe;
			this.ID = ID;
			this.resID = resID;

			if (toSpawn == null)
				this.toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			else
				this.toSpawn = toSpawn;
		}

		public BushPlantableTemplate SetBonusInfo(int minBonusSelection, float bonusChance = 0.01f)
		{
			this.minBonusSelection = minBonusSelection;
			this.bonusChance = bonusChance;
			return this;
		}

		public BushPlantableTemplate SetBonusSpawn(List<GameObject> bonusToSpawn)
		{
			this.bonusToSpawn = bonusToSpawn;
			return this;
		}

		public BushPlantableTemplate AddBonusSpawn(Identifiable.Id ID)
		{
			bonusToSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public BushPlantableTemplate AddBonusSpawn(GameObject obj)
		{
			bonusToSpawn.Add(obj);
			return this;
		}

		public BushPlantableTemplate SetSpawn(List<GameObject> toSpawn)
		{
			this.toSpawn = toSpawn;
			return this;
		}

		public BushPlantableTemplate AddSpawn(Identifiable.Id ID)
		{
			toSpawn.Add(GameContext.Instance.LookupDirector.GetPrefab(ID));
			return this;
		}

		public BushPlantableTemplate AddSpawn(GameObject obj)
		{
			toSpawn.Add(obj);
			return this;
		}

		public BushPlantableTemplate SetCustomTree(Mesh tree, Material[] treeMaterials, Mesh treeCol = null)
		{
			this.treeCol = treeCol ?? tree;
			this.tree = tree;
			this.treeMaterials = treeMaterials;
			return this;
		}

		public BushPlantableTemplate SetCustomTree(SpawnResource.Id ID)
		{
			treeID = ID;
			return this;
		}

		public BushPlantableTemplate SetCustomLeaves(Mesh leaves, Material[] leavesMaterials, Mesh leavesCol = null)
		{
			this.leavesCol = leavesCol ?? leaves;
			this.leaves = leaves;
			this.leavesMaterials = leavesMaterials;
			return this;
		}

		public BushPlantableTemplate SetCustomLeaves(SpawnResource.Id ID)
		{
			leavesID = ID;
			return this;
		}

		public BushPlantableTemplate SetLeavesPosition(Vector3 position)
		{
			SetLeavesPosition(position, position);
			return this;
		}

		public BushPlantableTemplate SetLeavesPosition(Vector3 position, Vector3 deluxePosition)
		{
			leavesPosition = position;
			leavesDeluxePosition = deluxePosition;
			customLeavePos = true;
			return this;
		}

		public BushPlantableTemplate SetSpawnInfo(int minSpawn, int maxSpawn, float minHours, float maxHours, float minNutrient = 20, float waterHours = 23)
		{
			this.minSpawn = minSpawn;
			this.maxSpawn = maxSpawn;
			this.minHours = minHours;
			this.maxHours = maxHours;
			this.minNutrient = minNutrient;
			this.waterHours = waterHours;
			return this;
		}

		public BushPlantableTemplate SetModel(Mesh modelMesh, Material[] modelMaterials)
		{
			this.modelMesh = modelMesh;
			this.modelMaterials = modelMaterials;
			return this;
		}

		public BushPlantableTemplate SetSpawnJoints(List<ObjectTransformValues> spawnJoints)
		{
			if ((spawnJoints.Count < 20 && !isDeluxe) || (spawnJoints.Count < 34 && isDeluxe))
			{
				Console.LogError($"Tried to register spawn joints for '<color=white>{mainObject.Name}</color>' but they are of an invalid size (20 for normal; 34 for deluxe)");
			}

			customSpawnJoints = spawnJoints;

			return this;
		}

		public override BushPlantableTemplate Create()
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
				})
			).SetAfterChildren(GrabJoints).SetAfterChildren(SetNetworkNodes);

			if (!isDeluxe)
			{
				mainObject.AddComponents(new ScaleMarker()
				{
					doNotScaleAsReplacement = false
				});
			}

			// Add network nodes
			GameObjectTemplate[] droneNetworkNodes = new GameObjectTemplate[4];

			for (int i = 0; i < 4; i++)
			{
				ObjectTransformValues trans = GardenObjects.droneNodes[i];
				droneNetworkNodes[i] = new GameObjectTemplate($"DroneNetworkNode{(i+1).ToString("00")}",
					new PathingNetworkNode()
				).AddChild(new GameObjectTemplate("NodeLoc").SetTransform(new Vector3(0, 2, 0), Vector3.zero, Vector3.one).SetDebugMarker(MarkerType.DroneNode))
				.SetAfterChildren((obj) => obj.GetComponent<PathingNetworkNode>().nodeLoc = obj.transform.GetChild(0))
				.SetTransform(new Vector3(trans.position.x * 1.5f, trans.position.y, trans.position.z * 1.5f), trans.rotation, trans.scale);
			}

			mainObject.AddChild(new GameObjectTemplate("DroneSubnetwork", new GardenDroneSubnetwork())
				.AddChild(droneNetworkNodes[0])
				.AddChild(droneNetworkNodes[1])
				.AddChild(droneNetworkNodes[2])
				.AddChild(droneNetworkNodes[3])
			);

			// Add tree
			GameObjectTemplate treeObj = new GameObjectTemplate("tree_bark",
				new Create<MeshFilter>((filter) => filter.sharedMesh = tree ?? (GardenObjects.modelTreeMeshs[treeID] ?? GardenObjects.modelTreeMeshs[SpawnResource.Id.POGO_TREE])),
				new Create<MeshRenderer>((render) => render.sharedMaterials = treeMaterials ?? GardenObjects.modelTreeMaterials[treeID] ?? GardenObjects.modelTreeMaterials[SpawnResource.Id.POGO_TREE]),
				new Create<MeshCollider>((col) =>
				{
					col.sharedMesh = treeCol ?? (GardenObjects.modelTreeCols[treeID] ?? GardenObjects.modelTreeCols[SpawnResource.Id.POGO_TREE]);
					col.convex = treeCol == tree;
				})
			).SetTransform(Vector3.up * -0.75f, Vector3.zero, Vector3.one * 0.5f);

			GameObjectTemplate leavesObj = new GameObjectTemplate("leaves_tree",
				new Create<MeshFilter>((filter) => filter.sharedMesh = leaves ?? (GardenObjects.modelLeavesMeshs[leavesID] ?? GardenObjects.modelLeavesMeshs[SpawnResource.Id.POGO_TREE])),
				new Create<MeshRenderer>((render) => render.sharedMaterials = leavesMaterials ?? GardenObjects.modelLeavesMaterials[leavesID] ?? GardenObjects.modelLeavesMaterials[SpawnResource.Id.POGO_TREE]),
				new Create<MeshCollider>((col) =>
				{
					col.sharedMesh = leavesCol ?? (GardenObjects.modelLeavesCols[leavesID] ?? GardenObjects.modelLeavesCols[SpawnResource.Id.POGO_TREE]);
					col.convex = leavesCol == leaves;
				})
			).SetTransform(customLeavePos ? leavesPosition : new Vector3(leavesPosition.x, leavesPosition.y - 0.75f, leavesPosition.z), Vector3.zero, Vector3.one);

			if (!isDeluxe)
			{
				mainObject.AddChild(treeObj.AddChild(leavesObj));
			}
			else
			{
				mainObject.AddChild(treeObj.AddComponents(new ScaleMarker()
				{
					doNotScaleAsReplacement = true
				}).AddChild(leavesObj));
			}

			// Add spawn joints
			for (int i = 0; i < 20; i++)
			{
				ObjectTransformValues trans = customSpawnJoints == null ? GardenObjects.treeSpawnJoints[leavesID][i] : customSpawnJoints[i];
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
				).SetTransform(customSpawnJoints != null ? trans.position : new Vector3(trans.position.x * 0.70f, trans.position.y - 1.55f, trans.position.z * 0.70f), trans.rotation, trans.scale)
				.SetDebugMarker(MarkerType.SpawnPoint)
				);
			}

			// Add Deluxe Stuff
			if (isDeluxe)
			{
				// Add spawn joints
				for (int i = 20; i < 34; i++)
				{
					ObjectTransformValues trans = customSpawnJoints == null ? GardenObjects.treeSpawnJoints[leavesID][i] : customSpawnJoints[i];
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
					).SetTransform(customSpawnJoints != null ? trans.position : new Vector3(trans.position.x, trans.position.y - 0.6f, trans.position.z), trans.rotation, trans.scale)
					.SetDebugMarker(MarkerType.SpawnPoint)
					);
				}

				// Add trees
				mainObject.AddChild(treeObj.Clone().AddComponents(new ScaleMarker()
				{
					doNotScaleAsReplacement = false
				}).SetTransform(new Vector3(3.8f, 0.2f, -3.8f), new Vector3(0, 225, 0), new Vector3(0.4f, 0.5f, 0.4f))
				.AddChild(leavesObj.Clone().SetTransform(customLeavePos ? leavesDeluxePosition : new Vector3(leavesDeluxePosition.x, leavesDeluxePosition.y - 0.6f, leavesDeluxePosition.z), Vector3.zero, new Vector3(1.3f, 0.9f, 1.3f))));

				mainObject.AddChild(treeObj.Clone().AddComponents(new ScaleMarker()
				{
					doNotScaleAsReplacement = false
				}).SetTransform(new Vector3(-3.8f, 0.2f, 3.8f), new Vector3(0, 45, 0), new Vector3(0.4f, 0.5f, 0.4f))
				.AddChild(leavesObj.Clone().SetTransform(customLeavePos ? leavesDeluxePosition : new Vector3(leavesDeluxePosition.x, leavesDeluxePosition.y - 0.6f, leavesDeluxePosition.z), Vector3.zero, new Vector3(1.3f, 0.9f, 1.3f))));
			}

			return this;
		}

		protected void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}

		protected void SetNetworkNodes(GameObject obj)
		{
			PathingNetworkNode[] nodes = obj.GetComponentsInChildren<PathingNetworkNode>();

			if (nodes.Length > 0)
			{
				foreach (PathingNetworkNode node in nodes)
				{
					if (node.connections == null)
						node.connections = new List<PathingNetworkNode>();
					else
						node.connections.Clear();
				}

				nodes[0].connections.Add(nodes[2]);
				nodes[0].connections.Add(nodes[3]);

				nodes[1].connections.Add(nodes[2]);
				nodes[1].connections.Add(nodes[3]);

				nodes[2].connections.Add(nodes[0]);
				nodes[2].connections.Add(nodes[1]);

				nodes[3].connections.Add(nodes[0]);
				nodes[3].connections.Add(nodes[1]);
			}
		}
	}
}
