using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new Craft Resources
	/// </summary>
	// TODO: FINISH THIS ONE
	public class FashionAttachTemplate : ModPrefab<FashionAttachTemplate>
	{
		// Base for Identifiables
		protected Identifiable.Id ID;
		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		// The Mesh and Materials
		protected Mesh mesh;
		protected Material[] materials;

		// The model transform
		protected ObjectTransformValues modelTrans = new ObjectTransformValues(Vector3.up * -0.2f, Vector3.zero, Vector3.one * 0.7f);

		// Component Configs
		protected Vector3 delaunchScale = Vector3.one;

		/// <summary>
		/// Template to create new Craft Resources
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommend, but not needed</param>
		/// <param name="ID">The Identifiable ID for this resource</param>
		/// <param name="mesh">The model's mesh for this resource</param>
		/// <param name="materials">The materials that compose this resource's model</param>
		public FashionAttachTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
		}

		/// <summary>
		/// Sets the vacuumable size
		/// </summary>
		/// <param name="vacSize">The vac size to set</param>
		public FashionAttachTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		/// <summary>
		/// Sets the transform values for the model
		/// </summary>
		/// <param name="trans">New values to set</param>
		public FashionAttachTemplate SetModelTrans(ObjectTransformValues trans)
		{
			modelTrans = trans;
			return this;
		}

		/// <summary>
		/// Sets the scale for the Delaunch Trigger (do not change if you don't know what you are doing)
		/// </summary>
		/// <param name="delaunchScale">The new scale to set</param>
		public FashionAttachTemplate SetDelaunchScale(Vector3 delaunchScale)
		{
			this.delaunchScale = delaunchScale;
			return this;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override FashionAttachTemplate Create()
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
			).SetTransform(Vector3.zero, Vector3.zero, delaunchScale));

			return this;
		}
	}
}
