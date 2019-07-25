using UnityEngine;

namespace SRMLExtras.Templates
{
	/// <summary>
	/// Interface used to make lists of Mod Prefabs (as lists can't have different generic constructs)
	/// </summary>
	public interface IModPrefab
	{
		GameObject ToPrefab();
		GameObjectTemplate AsTemplate();
		GameObjectTemplate AsTemplateClone();
	}

	/// <summary>
	/// Simple prefab like class, used to make the structure for templates
	/// </summary>
	public abstract class ModPrefab<T> : IModPrefab where T : ModPrefab<T>
	{
		protected GameObjectTemplate mainObject;
		private GameObject prefabVersion = null;

		private event System.Action<GameObjectTemplate> PrefabFunction;

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

		public T AddAwakeAction(string actionID)
		{
			mainObject.AddAwakeAction(actionID);
			return (T)this;
		}

		public T AddPrefabFunction(System.Action<GameObjectTemplate> action)
		{
			PrefabFunction += action;
			return (T)this;
		}

		public T AddPrefabFunction(params System.Action<GameObjectTemplate>[] actions)
		{
			foreach (System.Action<GameObjectTemplate> action in actions)
				PrefabFunction += action;

			return (T)this;
		}

		public GameObject ToPrefab()
		{
			if (prefabVersion == null)
			{
				PrefabFunction?.Invoke(mainObject);
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
