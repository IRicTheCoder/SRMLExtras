using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class OrnamentTemplate : ModPrefab<OrnamentTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Material[] materials;

		public OrnamentTemplate(string name, Identifiable.Id ID, Material[] materials) : base(name)
		{
			this.ID = ID;
			this.materials = materials;
		}

		public OrnamentTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public override OrnamentTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["quad_ornament"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials),
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
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["quad_ornament"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, new Vector3(0, 180, 0), Vector3.one * 0.8f));

			return this;
		}
	}
}
