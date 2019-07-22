using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new plorts
	/// </summary>
	public class PlortTemplate : ModPrefab<PlortTemplate>
	{
		// Base for Identifiables
		protected Identifiable.Id ID;
		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		// The Material
		protected Material[] materials;

		/// <summary>
		/// Template to create new plorts
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommended, but not needed)</param>
		/// <param name="ID">The Identifiable ID for this plort</param>
		/// <param name="materials">The materials that compose this plort's model</param>
		public PlortTemplate(string name, Identifiable.Id ID, Material[] materials) : base(name)
		{
			this.ID = ID;
			this.materials = materials;
		}

		/// <summary>
		/// Sets the vacuumable size
		/// </summary>
		/// <param name="vacSize">The vac size to set</param>
		public PlortTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override PlortTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["plort"]),
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
					body.angularDrag = 0.5f;
					body.mass = 0.3f;
					body.useGravity = true;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 1;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 10
				},
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new PlortInvulnerability()
				{
					invulnerabilityPeriod = 3
				},
				new PlaySoundOnHit()
				{
					hitCue = EffectObjects.hitPlort,
					minTimeBetween = 0.2f,
					minForce = 1,
					includeControllerCollisions = false
				},
				new DestroyOnIgnite()
				{
					igniteFX = EffectObjects.plortDespawn
				},
				new DestroyPlortAfterTime()
				{
					lifeTimeHours = 24,
					destroyFX = EffectObjects.plortDespawn
				}
			);

			// Create model
			mainObject.AddChild(new GameObjectTemplate("Shield",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["plort_shell"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.originMaterial["plortShield"].Group())
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.1f));

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

		internal void SetShield(GameObject obj)
		{
			obj.GetComponent<PlortInvulnerability>().activateObj = obj.FindChild("Shield");
		}
	}
}
