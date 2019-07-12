using UnityEngine;
using UnityEngine.SceneManagement;
using SRMLExtras.Patches;
using System.Collections.Generic;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// A base prefab made for mods
	/// </summary>
	public abstract class ModdedPrefab : IChild
	{
		public readonly static Dictionary<string, ModdedPrefab> registeredPrefabs = new Dictionary<string, ModdedPrefab>();

		private GameObject prefabObject = null;

		private string ID = "NoID";

		public ModdedGameObject MainObject { get; protected set; }

		public ModdedPrefab(string ID)
		{
			this.ID = ID;

			if (!registeredPrefabs.ContainsKey(ID))
				registeredPrefabs.Add(ID, this);
		}

		public abstract ModdedPrefab Create();

		public abstract void Setup(GameObject root);

		public virtual GameObject ToGameObject(GameObject root)
		{
			foreach (System.Type comp in MainObject.GetComponents())
				root.AddComponent(comp);
			root.tag = MainObject.Tag;
			root.layer = MainObject.Layer;
			root.name = MainObject.Name;

			RunThroughChildren(MainObject, root);

			Setup(root);

			return root;
		}

		internal virtual GameObject ToGameObjectChild(GameObject parent)
		{
			GameObject root = new GameObject(MainObject.Name);
			root.transform.parent = parent.transform;
			return ToGameObject(root);
		}

		public GameObject ToPrefabObject()
		{
			if (prefabObject == null)
			{
				Scene prev = SceneManager.GetActiveScene();
				SceneManager.SetActiveScene(ContentPatcher.prefabScene);
				prefabObject = new GameObject("!PREFAB: " + MainObject.Name, typeof(InstanciateOnAwake));
				prefabObject.GetComponent<InstanciateOnAwake>().prefab = ID;
				SceneManager.SetActiveScene(prev);
			}

			return prefabObject;
		}

		private void RunThroughChildren(ModdedGameObject modded, GameObject parent)
		{
			foreach (IChild child in modded.children)
			{
				if (child is ModdedGameObject)
				{
					ModdedGameObject go = (ModdedGameObject)child;
					GameObject c = go.ToGameObject(parent.transform);

					if (go.children.Count > 0)
						RunThroughChildren(go, c);
				}

				if (child is ModdedPrefab)
				{
					ModdedPrefab go = (ModdedPrefab)child;
					go.ToGameObjectChild(parent);
				}
			}
		}
	}
}
