using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class PlortTemplate : ModPrefab<PlortTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Material[] materials;

		public PlortTemplate(string name, Identifiable.Id ID, Material[] materials) : base(name)
		{
			this.ID = ID;
			this.materials = materials;
		}

		public PlortTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

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
				new PlortInstability()
				{
					lifetimeHours = 0.5f,
					explodePower = 400,
					explodeRadius = 7,
					explodeFX = EffectObjects.explosion,
					minPlayerDamage = 10,
					maxPlayerDamage = 10
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
