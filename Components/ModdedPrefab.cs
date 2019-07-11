using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Components
{
	/// <summary>
	/// A base prefab made for mods
	/// </summary>
	public abstract class ModdedPrefab
	{
		protected ModdedGameObject mainObject;

		public abstract void Setup();

		public virtual GameObject ToGameObject()
		{
			Setup();

			GameObject root = mainObject.ToGameObject(null);
			RunThroughChildren(mainObject.Transform);

			return root;
		}

		private void RunThroughChildren(ModdedTransform parent)
		{
			foreach (ModdedTransform child in parent)
			{
				if (child.childCount > 0)
					RunThroughChildren(child);

				child.ModdedObject.ToGameObject(parent);
			}
		}
	}
}
