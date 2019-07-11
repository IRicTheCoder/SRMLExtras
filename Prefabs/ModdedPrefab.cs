using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// A base prefab made for mods
	/// </summary>
	public abstract class ModdedPrefab
	{
		private GameObject prefabObject;

		public ModdedGameObject MainObject { get; protected set; }

		public abstract void Create();

		public abstract void Setup(GameObject root);

		public virtual GameObject ToGameObject(GameObject root)
		{
			foreach (System.Type comp in MainObject.GetComponents())
				root.AddComponent(comp);

			RunThroughChildren(MainObject, root);

			Setup(root);

			return root;
		}

		public GameObject ToPrefabObject()
		{
			// TODO: Arranja esta merda
			return null;
		}

		private void RunThroughChildren(ModdedGameObject modded, GameObject parent)
		{
			foreach (ModdedGameObject child in modded.children)
			{
				GameObject c = child.ToGameObject(parent.transform);

				if (child.children.Count > 0)
					RunThroughChildren(child, c);
			}
		}
	}
}
