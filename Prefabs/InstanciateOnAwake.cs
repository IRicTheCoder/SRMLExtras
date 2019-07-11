using UnityEngine;
using SRMLExtras.Patches;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// Instanciates a modded prefab on awake
	/// </summary>
	public class InstanciateOnAwake : MonoBehaviour
	{
		public ModdedPrefab prefab;

		public void Awake()
		{
			if (gameObject.scene == ContentPatcher.prefabScene)
				return;

			// TODO: Faz merdas
		}
	}
}
