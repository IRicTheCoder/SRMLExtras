using System.Collections.Generic;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// Interface used to make lists of PlotUpgradeTemplate (as lists can't have different generic constructs)
	/// </summary>
	public interface IPlotUpgradeTemplate
	{
		ICreateComponent GetUpgrader();
	}

	/// <summary>
	/// A template to create a plot upgrade
	/// <para>This template is a bit more complex than the others, it's structure is entirely built by you. It only exists for covinience when adding to custom plots.</para>
	/// </summary>
	public class PlotUpgradeTemplate<T> : ModPrefab<PlotUpgradeTemplate<T>>, IPlotUpgradeTemplate where T : PlotUpgrader
	{
		// Base for Plot Upgrade
		protected LandPlot.Upgrade upgrade;
		protected Create<T> upgrader;

		/// <summary>
		/// Template to create a plot frame
		/// </summary>
		/// <param name="name">The name of the object</param>
		public PlotUpgradeTemplate(string name, LandPlot.Upgrade upgrade, Create<T> upgrader) : base(name)
		{
			this.upgrade = upgrade;
			this.upgrader = upgrader;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		[System.Obsolete("For the plot upgrade use .Create(action) to customize the upgrade")]
		public override PlotUpgradeTemplate<T> Create()
		{
			return null;
		}

		/// <summary>
		/// Creates the object of the template (To get the prefab version use .ToPrefab() after calling this)
		/// </summary>
		/// <param name="action">The action to construct the template</param>
		public PlotUpgradeTemplate<T> Create(System.Action<GameObjectTemplate> action)
		{
			action?.Invoke(mainObject);
			return this;
		}

		ICreateComponent IPlotUpgradeTemplate.GetUpgrader()
		{
			return upgrader;
		}
	}
}
