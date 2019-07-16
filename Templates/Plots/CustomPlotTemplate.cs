using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class CustomPlotTemplate : ModPrefab<CustomPlotTemplate>
	{
		protected List<GameObjectTemplate> attachments = new List<GameObjectTemplate>();

		protected LandPlot.Id ID;

		protected ObjectTransformValues techActivatorTrans = new ObjectTransformValues(new Vector3(-5.5f, -0.1f, -5.5f), new Vector3(0, 255, 0), Vector3.one);
		protected GameObject prefabUI;

		public CustomPlotTemplate(string name, LandPlot.Id ID) : base(name)
		{
			this.ID = ID;
		}

		public CustomPlotTemplate SetActivatorTransform(Vector3 position, Vector3 rotation, Vector3 scale)
		{
			techActivatorTrans = new ObjectTransformValues(position, rotation, scale);
			return this;
		}

		public CustomPlotTemplate SetActivatorTransform(ObjectTransformValues trans)
		{
			techActivatorTrans = trans;
			return this;
		}

		public CustomPlotTemplate SetPlotUI(GameObject plotUI)
		{
			prefabUI = plotUI;
			return this;
		}

		public override CustomPlotTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new LandPlot()
				{
					typeId = ID
				},
				new Recolorizer()
			);

			// Add Tech Activator
			mainObject.AddChild(new TechActivatorTemplate("techActivator", prefabUI).AsTemplate().SetTransform(techActivatorTrans));

			// Add Plot Structure (Frame & Dirt)
			mainObject.AddChild(new PlotFrameTemplate("Frame").AsTemplate());

			mainObject.AddChild(new GameObjectTemplate("Dirt",
				new Create<MeshFilter>((filter) => filter.sharedMesh = RanchObjects.dirtMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = RanchObjects.dirtMaterials)
			).SetTransform(new Vector3(4.8f, 0, -4.8f), Vector3.zero, Vector3.one)
			.AddChild(new GameObjectTemplate("rocks",
				new Create<MeshFilter>((filter) => filter.sharedMesh = RanchObjects.rocksMesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = RanchObjects.rocksMaterials)
			))
			.AddChild(new GameObjectTemplate("plane",
				new Create<MeshFilter>((filter) => filter.sharedMesh = RanchObjects.plotPlane),
				new Create<MeshRenderer>((render) => render.sharedMaterials = RanchObjects.plotPlaneMaterials)
			).SetTransform(new Vector3(-4.8f, 0, 4.8f), Vector3.right * 90, Vector3.one * 9f)
			));

			return this;
		}

		protected void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}
	}
}
