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

		public CustomPlotTemplate(string name, LandPlot.Id ID) : base(name)
		{
			this.ID = ID;
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

			return this;
		}

		protected void GrabJoints(GameObject obj)
		{
			obj.GetComponent<SpawnResource>().SpawnJoints = obj.GetComponentsInChildren<FixedJoint>();
		}
	}
}
