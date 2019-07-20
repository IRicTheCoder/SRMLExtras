using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class LiquidTemplate : ModPrefab<LiquidTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Material[] materials;

		public LiquidTemplate(string name, Identifiable.Id ID, Material[] materials = null) : base(name)
		{
			this.ID = ID;
			this.materials = materials ?? BaseObjects.originMaterial["Depth Water Ball"].Group();
		}

		public LiquidTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public override LiquidTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Identifiable()
				{
					id = ID
				},
				new Vacuumable()
				{
					size = vacSize
				},
				new Create<Rigidbody>((body) =>
				{
					body.drag = 0.2f;
					body.angularDrag = 0.05f;
					body.mass = 1f;
					body.useGravity = true;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 1f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new DestroyOnTouching()
				{
					hoursOfContactAllowed = 0,
					wateringRadius = 4,
					wateringUnits = 3,
					destroyFX = EffectObjects.waterSplat,
					touchingWaterOkay = false,
					touchingAshOkay = false,
					reactToActors = false,
					liquidType = ID
				}
			);

			// Create sphere
			mainObject.AddChild(new GameObjectTemplate("Sphere",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["slimeglop"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.5f)
			.AddAfterChildren(AddEffects));

			// Create delaunch
			mainObject.AddChild(new GameObjectTemplate("DelaunchTrigger",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new VacDelaunchTrigger()
			));

			return this;
		}

		internal void AddEffects(GameObject obj)
		{
			GameObject fx1 = BaseObjects.originFXs["FX Sprinkler 1"].CreatePrefabCopy();
			GameObject fx2 = BaseObjects.originFXs["FX Water Glops"].CreatePrefabCopy();

			fx1.transform.parent = obj.transform;
			fx2.transform.parent = obj.transform;
		}
	}
}
