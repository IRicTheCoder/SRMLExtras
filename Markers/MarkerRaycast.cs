using UnityEngine;

namespace SRMLExtras.Markers
{
	public class MarkerRaycast : MonoBehaviour
	{
		void Update()
		{
			if (!DebugCommand.DebugMode)
				return;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			bool def = Physics.queriesHitTriggers;
			Physics.queriesHitTriggers = true;

			foreach (RaycastHit hit in Physics.RaycastAll(ray, 5f))
			{
				if (hit.collider.GetComponent<DebugMarker>() != null)
					hit.collider.GetComponent<DebugMarker>().SetHover();
			}

			Physics.queriesHitTriggers = def;
		}
	}
}
