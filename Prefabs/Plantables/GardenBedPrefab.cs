using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SRMLExtras.Prefabs
{
	public class GardenBedPrefab : ModdedPrefab
	{
		internal static Mesh dirtMesh;
		internal static Mesh rocksMesh;

		internal static Mesh defaultSprout;
		internal static Material[] defaultSproutMaterial;

		internal static Material[] dirtMaterialVeggie;
		internal static Material[] dirtMaterialFruit;

		internal static Material[] rocksMaterialVeggie;
		internal static Material[] rocksMaterialFruit;

		internal static bool populate = false;

		private string bedName = "Bed";
		private Vector3 position = Vector3.zero;
		private bool veggie = true;

		private Mesh sprout;
		private Material[] sproutMaterials;

		public GardenBedPrefab(string bedName, Vector3 position, bool veggie, bool intern) : base(bedName, intern)
		{
			this.bedName = bedName;
			this.position = position;
			this.veggie = veggie;
		}

		public GardenBedPrefab(string bedName, Vector3 position, bool veggie, Mesh sprout, Material[] sproutMaterials, bool intern) : base(bedName, intern)
		{
			this.bedName = bedName;
			this.position = position;
			this.veggie = veggie;
			this.sprout = sprout;
			this.sproutMaterials = sproutMaterials;
		}

		public override ModdedPrefab Create()
		{
			MainObject = new ModdedGameObject(bedName, typeof(LODGroup),
				typeof(CapsuleCollider));

			ModdedGameObject dirt = new ModdedGameObject("Dirt", typeof(MeshFilter),
				typeof(MeshRenderer));

			ModdedGameObject rocks = new ModdedGameObject("rocks_long", typeof(MeshFilter),
				typeof(MeshRenderer));

			dirt.AddChild(rocks);
			MainObject.AddChild(dirt);

			MainObject.AddChild(new ModdedGameObject("Sprout1", typeof(MeshFilter), typeof(MeshRenderer)));
			MainObject.AddChild(new ModdedGameObject("Sprout2", typeof(MeshFilter), typeof(MeshRenderer)));
			MainObject.AddChild(new ModdedGameObject("Sprout3", typeof(MeshFilter), typeof(MeshRenderer)));
			MainObject.AddChild(new ModdedGameObject("Sprout4", typeof(MeshFilter), typeof(MeshRenderer)));

			return this;
		}

		public override void Setup(GameObject root)
		{
			// Setup the main object
			root.transform.localPosition = position;

			LODGroup lod = root.GetComponent<LODGroup>();
			lod.localReferencePoint = Vector3.one * 0.1f;
			lod.size = 8.657982f;
			lod.SetPrivateProperty("lodCount", 3);
			lod.fadeMode = LODFadeMode.None;
			lod.animateCrossFading = false;

			CapsuleCollider col = root.GetComponent<CapsuleCollider>();
			col.center = new Vector3(0, -0.6f, 0);
			col.radius = 0.8f;
			col.height = 8;
			col.direction = 2;
			col.contactOffset = 0.01f;

			// Setup dirt
			GameObject dirt = root.transform.Find("Dirt").gameObject;
			dirt.GetComponent<MeshFilter>().sharedMesh = dirtMesh;
			dirt.GetComponent<MeshRenderer>().sharedMaterials = veggie ? dirtMaterialVeggie : dirtMaterialFruit;

			// Setup Rocks
			GameObject rocks = dirt.transform.Find("rocks_long").gameObject;
			rocks.GetComponent<MeshFilter>().sharedMesh = rocksMesh;
			rocks.GetComponent<MeshRenderer>().sharedMaterials = veggie ? rocksMaterialVeggie : rocksMaterialFruit;

			// Setup Sprouts
			GameObject sprout1 = root.transform.Find("Sprout1").gameObject;
			sprout1.transform.localPosition = new Vector3(0, 0.1f, -3f);
			sprout1.transform.eulerAngles = new Vector3(351.2f, 0, 0);
			sprout1.GetComponent<MeshFilter>().sharedMesh = sprout == null ? defaultSprout : sprout;
			sprout1.GetComponent<MeshRenderer>().sharedMaterials = sprout == null ? defaultSproutMaterial : sproutMaterials;
			sprout1.name = "Sprout";

			GameObject sprout2 = root.transform.Find("Sprout2").gameObject;
			sprout2.transform.localPosition = new Vector3(0.4f, 0, -1.3f);
			sprout2.transform.eulerAngles = new Vector3(354.8f, 6.2f, 348.4f);
			sprout2.GetComponent<MeshFilter>().sharedMesh = sprout == null ? defaultSprout : sprout;
			sprout2.GetComponent<MeshRenderer>().sharedMaterials = sprout == null ? defaultSproutMaterial : sproutMaterials;
			sprout2.name = "Sprout";

			GameObject sprout3 = root.transform.Find("Sprout3").gameObject;
			sprout3.transform.localPosition = new Vector3(-0.5f, 0.1f, 1.6f);
			sprout3.transform.eulerAngles = new Vector3(347.6f, 76.4f, 357.1f);
			sprout3.GetComponent<MeshFilter>().sharedMesh = sprout == null ? defaultSprout : sprout;
			sprout3.GetComponent<MeshRenderer>().sharedMaterials = sprout == null ? defaultSproutMaterial : sproutMaterials;
			sprout3.name = "Sprout";

			GameObject sprout4 = root.transform.Find("Sprout4").gameObject;
			sprout4.transform.localPosition = new Vector3(0.3f, 0.1f, 2.4f);
			sprout4.transform.eulerAngles = new Vector3(3.5f, 353.1f, 354.3f);
			sprout4.GetComponent<MeshFilter>().sharedMesh = sprout == null ? defaultSprout : sprout;
			sprout4.GetComponent<MeshRenderer>().sharedMaterials = sprout == null ? defaultSproutMaterial : sproutMaterials;
			sprout4.name = "Sprout";
		}

		public static void PopulateDefaults(GameObject veggie, GameObject fruit)
		{
			if (populate)
				return;

			GameObject dirt = veggie.transform.Find("Bed/Dirt").gameObject;
			dirtMesh = dirt.GetComponent<MeshFilter>().sharedMesh;

			GameObject rocks = veggie.transform.Find("Bed/Dirt/rocks_long").gameObject;
			rocksMesh = rocks.GetComponent<MeshFilter>().sharedMesh;

			GameObject sprout = veggie.transform.Find("Bed/Sprout").gameObject;
			defaultSprout = sprout.GetComponent<MeshFilter>().sharedMesh;
			defaultSproutMaterial = sprout.GetComponent<MeshRenderer>().sharedMaterials;

			dirtMaterialVeggie = dirt.GetComponent<MeshRenderer>().sharedMaterials;
			rocksMaterialVeggie = rocks.GetComponent<MeshRenderer>().sharedMaterials;

			populate = true;
		}
	}
}
