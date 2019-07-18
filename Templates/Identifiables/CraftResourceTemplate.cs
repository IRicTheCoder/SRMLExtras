using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class CraftResourceTemplate : ModPrefab<CraftResourceTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Mesh mesh;
		protected Material[] materials;

		protected ObjectTransformValues modelTrans = new ObjectTransformValues(Vector3.up * -0.2f, Vector3.zero, Vector3.one * 0.7f);

		public CraftResourceTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
		}

		public CraftResourceTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public CraftResourceTemplate SetModelTrans(ObjectTransformValues trans)
		{
			modelTrans = trans;
			return this;
		}

		public override CraftResourceTemplate Create()
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
					body.angularDrag = 5f;
					body.mass = 0.3f;
					body.useGravity = true;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 10
				},
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.25f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				}
			);

			// Create model
			mainObject.AddChild(new GameObjectTemplate("resource_ld",
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(modelTrans));

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
	}
}
