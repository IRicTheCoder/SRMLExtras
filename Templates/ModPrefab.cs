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

		public ModPrefab(string name)
		{
			mainObject = new GameObjectTemplate(name);
		}

		public abstract T Create();

		public GameObject ToPrefab()
		{
			if (prefabVersion == null)
				prefabVersion = mainObject.ToGameObject(null);

			return prefabVersion;
		}
	}
}
