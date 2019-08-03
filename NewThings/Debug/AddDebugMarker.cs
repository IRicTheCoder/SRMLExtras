using UnityEngine;

namespace SRMLExtras.Debug
{
	public class AddDebugMarker : MonoBehaviour
	{
		public MarkerType type;
		public float scaleMult;

		public void Start()
		{
			gameObject.SetDebugMarker(type, scaleMult);
			Destroy(this);
		}
	}
}
