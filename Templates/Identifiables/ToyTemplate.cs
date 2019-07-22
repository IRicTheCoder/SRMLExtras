using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new toys
	/// </summary>
	public class ToyTemplate : ModPrefab<ToyTemplate>
	{
		// Base for Identifiables
		protected Identifiable.Id ID;
		protected Vacuumable.Size vacSize = Vacuumable.Size.LARGE;

		// The Mesh and Materials
		protected Mesh mesh;
		protected Material[] materials;

		// Toy Specific
		protected SECTR_AudioCue hitCue;
		protected Identifiable.Id fashion = Identifiable.Id.NONE;

		/// <summary>
		/// Template to create new toys
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommended, but not needed)</param>
		/// <param name="ID">The Identifiable ID for this toy</param>
		/// <param name="mesh">The model's mesh for this toy</param>
		/// <param name="materials">The materials that compose this toy's model</param>
		/// <param name="hitCue">The audio cue when this toy hits something</param>
		public ToyTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials, SECTR_AudioCue hitCue) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
			this.hitCue = hitCue;
		}

		/// <summary>
		/// Sets the vacuumable size
		/// </summary>
		/// <param name="vacSize">The vac size to set</param>
		public ToyTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		/// <summary>
		/// Sets what fashion is required to react with this toy
		/// </summary>
		/// <param name="ID">The ID of said fashion (NONE to remove the required fashion)</param>
		public ToyTemplate SetRequiredFashion(Identifiable.Id ID)
		{
			fashion = ID;
			return this;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override ToyTemplate Create()
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
					body.angularDrag = 0.5f;
					body.mass = 0.5f;
					body.useGravity = true;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.5f;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 25f
				},
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new StopOnCollision()
				{
					distFromCol = 0.25f
				},
				new TotemLinkerHelper(),
				new PlaySoundOnHit()
				{
					hitCue = hitCue,
					minTimeBetween = 0.3f,
					minForce = 0.1f,
					includeControllerCollisions = false
				}
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.3f);

			// Create Totem Linker
			mainObject.AddChild(new GameObjectTemplate("TotemLinker",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new TotemLinker()
				{
					receptivenessProb = 0.25f,
					rethinkReceptivenessMin = 6,
					rethinkReceptivenessMax = 12,
					gravFactorWhileTotemed = 0.5f
				}
			));

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

			// Create influence
			mainObject.AddChild(new GameObjectTemplate("Influence",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 5f;
					col.isTrigger = true;
				}),
				new ToyProximityTrigger()
				{
					fashion = fashion
				}
			));

			// Create model
			mainObject.AddChild(new GameObjectTemplate("resource_ld",
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(new Vector3(0, -0.5f, 0), Vector3.zero, Vector3.one * 0.5f));

			return this;
		}
	}
}
