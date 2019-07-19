using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	// TODO: Finish this
	public class AnimalTemplate : ModPrefab<AnimalTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.LARGE;

		protected Mesh mesh;
		protected Material[] materials;

		protected Material rottenMaterial;

		protected Component moveComponent;

		protected int minRepHours = 8;
		protected int maxRepHours = 16;
		protected int edibleHours = 36;
		protected int rottenHours = 6;

		protected Identifiable mate;

		public AnimalTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials, Identifiable mate) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
			this.mate = mate;

			moveComponent = new ChickenRandomMove()
			{
				maxJump = 1f,
				walkForwardForce = 3.333f,
				flapCue = EffectObjects.flapCue
			};
		}

		public AnimalTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public AnimalTemplate SetResourceInfo(int unripeGameHours, int ripeGameHours, int edibleGameHours = 36, int rottenGameHours = 6)
		{
			/*unripeHours = unripeGameHours;
			ripeHours = ripeGameHours;
			edibleHours = edibleGameHours;
			rottenHours = rottenGameHours;*/
			return this;
		}

		public AnimalTemplate SetRottenMaterial(Material rotten)
		{
			rottenMaterial = rotten;
			return this;
		}

		public override AnimalTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
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
					body.drag = 0.2f;
					body.angularDrag = 5f;
					body.mass = 0.3f;
					body.useGravity = true;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 10
				},
				new Create<BoxCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.size = Vector3.one * 0.5f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new Create<SECTR_PointSource>((source) =>
				{
					source.Loop = false;
					source.PlayOnStart = false;
					source.RestartLoopsOnEnabled = true;
					//source.SetPrivateField("instance", EffectObjects.fruitCueInstance);
					source.Volume = 1;
					source.Pitch = 1;
				}),
				new PlaySoundOnHit()
				{
					hitCue = EffectObjects.hitFruit,
					minTimeBetween = 0.2f,
					minForce = 1,
					includeControllerCollisions = false
				},
				new SlimeSubbehaviourPlexer()
				{

				},
				moveComponent,
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.5f;
					col.isTrigger = true;
				}),
				new SlimeAudio()
				{
					slimeSounds = BaseObjects.originSounds["HenHen"]
				},
				new Reproduce()
				{
					nearMateId = mate,
					maxDistToMate = 10f,
					densityDist = 10,
					maxDensity = 12,
					deluxeDensityFactor = 2
				}
			);

			// Create delaunch trigger
			mainObject.AddChild(new GameObjectTemplate("DelaunchTrigger",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new VacDelaunchTrigger()
			));

			// Create model
			mainObject.AddChild(new GameObjectTemplate("model_fruit",
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 0.3f));

			// Create shadow
			mainObject.AddChild(new GameObjectTemplate("Shadow",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["sphere48"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.originMaterial["Default-Material"].Group())
			));

			return this;
		}
	}
}
