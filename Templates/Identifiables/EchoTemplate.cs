using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class EchoTemplate : ModPrefab<EchoTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Material[] materials;

		public EchoTemplate(string name, Identifiable.Id ID, Material[] materials = null) : base(name)
		{
			this.ID = ID;
			this.materials = materials ?? BaseObjects.originMaterial["EchoBlue"].Group();
		}

		public EchoTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public override EchoTemplate Create()
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
					body.drag = 5f;
					body.angularDrag = 0.5f;
					body.mass = 0.3f;
					body.useGravity = false;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.25f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new StopOnCollision()
				{
					distFromCol = 0.25f
				}
			);

			// Create model
			mainObject.AddChild(new GameObjectTemplate("model",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["Quad"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.7f));

			return this;
		}
	}
}
