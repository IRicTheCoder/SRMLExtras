using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// Simple prefab like class, used to make the structure for templates
	/// </summary>
	public abstract class ModPrefab<T> where T : ModPrefab<T>
	{
		protected GameObjectTemplate mainObject;
		private GameObject prefabVersion;

		private event System.Action<GameObjectTemplate> prefabFunction;

		public ModPrefab(string name)
		{
			mainObject = new GameObjectTemplate(name);
		}

		public abstract T Create();

		public T AddStartAction(string actionID)
		{
			mainObject.AddStartAction(actionID);
			return (T)this;
		}

		public T AddPrefabFunction(System.Action<GameObjectTemplate> action)
		{
			prefabFunction += action;
			return (T)this;
		}

		public T AddPrefabFunction(params System.Action<GameObjectTemplate>[] actions)
		{
			foreach (System.Action<GameObjectTemplate> action in actions)
				prefabFunction += action;

			return (T)this;
		}

		public GameObject ToPrefab()
		{
			if (prefabVersion == null)
			{
				prefabFunction?.Invoke(mainObject);
				prefabVersion = mainObject.ToGameObject(null);
			}

			return prefabVersion;
		}

		public GameObjectTemplate AsTemplate()
		{
			return mainObject;
		}

		public GameObjectTemplate AsTemplateClone()
		{
			return mainObject.Clone();
		}
	}
}
