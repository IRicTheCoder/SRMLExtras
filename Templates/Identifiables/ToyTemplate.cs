using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class ToyTemplate : ModPrefab<ToyTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.LARGE;

		protected Mesh mesh;
		protected Material[] materials;
		protected SECTR_AudioCue cue;

		protected Identifiable.Id fashion = Identifiable.Id.NONE;

		public ToyTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials, SECTR_AudioCue cue) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
			this.cue = cue;
		}

		public ToyTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public ToyTemplate SetRequiredFashion(Identifiable.Id ID)
		{
			fashion = ID;
			return this;
		}

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
					hitCue = cue,
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
