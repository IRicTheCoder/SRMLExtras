using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// A template to create new fashion attachments (the part that gets attached to the object)
	/// </summary>
	public class FashionAttachTemplate : ModPrefab<FashionAttachTemplate>
	{
		// The Mesh and Materials
		protected Mesh mesh;
		protected Material[] materials;

		// The model transform
		protected ObjectTransformValues modelTrans = new ObjectTransformValues(Vector3.up * -0.2f, Vector3.zero, Vector3.one * 0.7f);

		/// <summary>
		/// Template to create new fashion attachments
		/// </summary>
		/// <param name="name">The name of the object (prefixes are recommend, but not needed)</param>
		/// <param name="ID">The Identifiable ID for this fashion attachment</param>
		/// <param name="mesh">The model's mesh for this fashion attachment</param>
		/// <param name="materials">The materials that compose this fashion attachment's model</param>
		public FashionAttachTemplate(string name, Mesh mesh, Material[] materials) : base(name)
		{
			this.mesh = mesh;
			this.materials = materials;
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
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		public override FashionAttachTemplate Create()
		{
			// Create model
			mainObject.AddChild(new GameObjectTemplate("model_fp",
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(modelTrans));

			return this;
		}
	}
}
