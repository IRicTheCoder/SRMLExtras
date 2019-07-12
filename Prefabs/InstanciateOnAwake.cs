using UnityEngine;
using SRMLExtras.Patches;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// Instanciates a modded prefab on awake
	/// </summary>
	public class InstanciateOnAwake : MonoBehaviour
	{
		public string prefab;

		public void Awake()
		{
			SRML.Console.Console.Log("PrefabState: " + prefab);

			if (gameObject.scene == ContentPatcher.prefabScene)
				return;

			SRML.Console.Console.Log("InstanciateOnAwake - GO: " + gameObject + " - Contains: " + ModdedPrefab.registeredPrefabs.ContainsKey(prefab));
			ModdedPrefab.registeredPrefabs[prefab].ToGameObject(gameObject);

			Destroy(this);
		}
	}
}
