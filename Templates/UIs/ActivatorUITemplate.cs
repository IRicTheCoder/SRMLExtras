using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class ActivatorUITemplate : ModPrefab<ActivatorUITemplate>
	{
		protected BaseUI baseUI;

		public ActivatorUITemplate(string name, BaseUI baseUI) : base(name)
		{
			this.baseUI = baseUI;
		}

		public override ActivatorUITemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				baseUI,
				new UIInputLocker()
				{
					lockEvenSpecialScenes = false
				}
			);

			return this;
		}
	}
}
