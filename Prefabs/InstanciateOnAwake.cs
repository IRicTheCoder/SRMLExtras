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
			if (gameObject.scene == ContentPatcher.prefabScene)
				return;

			ModdedPrefab.registeredPrefabs[prefab].ToGameObject(gameObject);
			Destroy(this);
		}
	}
}
