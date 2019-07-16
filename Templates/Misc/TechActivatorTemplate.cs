using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class TechActivatorTemplate : ModPrefab<TechActivatorTemplate>
	{
		protected GameObject prefabUI;

		public TechActivatorTemplate(string name, GameObject prefabUI) : base(name)
		{
			this.prefabUI = prefabUI;
		}

		public override TechActivatorTemplate Create()
		{
			// Create the structure
			mainObject.AddChild(new GameObjectTemplate("techActivator",
				new Create<MeshFilter>((filter) => filter.sharedMesh = RanchObjects.techActivator),
				new Create<MeshRenderer>((render) => render.sharedMaterials = RanchObjects.techActivatorMaterials),
				new Create<CapsuleCollider>((col) =>
				{
					col.center = new Vector3(0, 0.8f, 0);
					col.radius = 0.2767721f;
					col.height = 1.578097f;
					col.direction = 1;
				})
			));

			// Create the trigger
			mainObject.AddChild(new GameObjectTemplate("triggerActivate",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.5f;
				}),
				new UIActivator()
				{
					uiPrefab = prefabUI,
					blockInExpoPrefab = null
				}
			).SetTransform(Vector3.up * 1.4f, Vector3.zero, Vector3.one * 0.7f));

			return this;
		}

		protected void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}
	}
}
