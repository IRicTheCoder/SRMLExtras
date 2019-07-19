using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class CrateTemplate : ModPrefab<CrateTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.LARGE;

		protected Material[] materials;

		protected List<BreakOnImpact.SpawnOption> spawnOptions = new List<BreakOnImpact.SpawnOption>();

		protected ObjectTransformValues modelTrans = new ObjectTransformValues(Vector3.up * -0.2f, Vector3.zero, Vector3.one * 0.7f);

		public CrateTemplate(string name, Identifiable.Id ID, Material[] materials = null) : base(name)
		{
			this.ID = ID;
			this.materials = materials ?? BaseObjects.originMaterial["objWoodKit01"].Group();
		}

		public CrateTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public CrateTemplate SetModelTrans(ObjectTransformValues trans)
		{
			modelTrans = trans;
			return this;
		}

		public CrateTemplate SetSpawnOptions(List<BreakOnImpact.SpawnOption> spawnOptions)
		{
			this.spawnOptions = spawnOptions;
			return this;
		}

		public override CrateTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["objCrate"]),
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
					body.mass = 1f;
					body.useGravity = true;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 10
				},
				new Create<BoxCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.size = Vector3.one;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new BreakOnImpact()
				{
					minSpawns = 4,
					maxSpawns = 6,
					breakFX = EffectObjects.crateBrake,
					spawnOptions = spawnOptions
				}
			);

			return this;
		}
	}
}
