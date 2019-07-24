using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new liquids
	/// </summary>
	public class LiquidTemplate : ModPrefab<LiquidTemplate>
	{
		// Base for Identifiables
		protected Identifiable.Id ID;
		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		// The Materials and Color
		protected Material[] materials;
		protected Color color = Color.clear;

		/// <summary>
		/// Template to create new liquids
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommended, but not needed)</param>
		/// <param name="ID">The Identifiable ID for this liquid</param>
		/// <param name="materials">The materials that compose this liquid's model</param>
		public LiquidTemplate(string name, Identifiable.Id ID, Material[] materials = null) : base(name)
		{
			this.ID = ID;
			this.materials = materials ?? BaseObjects.originMaterial["Depth Water Ball"].Group();
		}

		/// <summary>
		/// Sets the vacuumable size
		/// </summary>
		/// <param name="vacSize">The vac size to set</param>
		public LiquidTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		/// <summary>
		/// Sets the color for the material (only if you want to use the default material for water and change the color)
		/// </summary>
		/// <param name="color">New color to set</param>
		public LiquidTemplate SetColor(Color color)
		{
			this.color = color;
			return this;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override LiquidTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Create<Identifiable>((ident) => ident.id = ID),
				new Create<Vacuumable>((vac) => vac.size = vacSize),
				new Create<Rigidbody>((body) =>
				{
					body.drag = 0.2f;
					body.angularDrag = 0.05f;
					body.mass = 1f;
					body.useGravity = true;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 1f;
				}),
				new Create<CollisionAggregator>(null),
				new Create<RegionMember>((rg) => rg.canHibernate = true),
				new Create<DestroyOnTouching>((dest) =>
				{
					dest.hoursOfContactAllowed = 0;
					dest.wateringRadius = 4;
					dest.wateringUnits = 3;
					dest.destroyFX = EffectObjects.fxWaterSplat;
					dest.touchingWaterOkay = false;
					dest.touchingAshOkay = false;
					dest.reactToActors = false;
					dest.liquidType = ID;
				})
			);

			// Create sphere
			if (color != Color.clear)
			{
				Material mat = new Material(BaseObjects.originMaterial["Depth Water Ball"]);
				mat.SetColor("_Color", color);

				materials = mat.Group();
			}

			mainObject.AddChild(new GameObjectTemplate("Sphere",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["slimeglop"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.5f)
			.AddAfterChildren(AddEffects));

			// Create delaunch
			mainObject.AddChild(new GameObjectTemplate("DelaunchTrigger",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new Create<VacDelaunchTrigger>(null)
			));

			return this;
		}

		protected void AddEffects(GameObject obj)
		{
			GameObject fx1 = BaseObjects.originFXs["FX Sprinkler 1"].CreatePrefabCopy();
			GameObject fx2 = BaseObjects.originFXs["FX Water Glops"].CreatePrefabCopy();

			fx1.transform.parent = obj.transform;
			fx2.transform.parent = obj.transform;
		}
	}
}
