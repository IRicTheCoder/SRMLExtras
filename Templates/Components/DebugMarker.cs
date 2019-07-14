using UnityEngine;

namespace SRMLExtras.Templates
{
	public class DebugMarker : MonoBehaviour
	{
		private MeshRenderer mesh;
		public MarkerType type;
		public float scaleMult;

		private bool hover = false;
		private string objName = string.Empty;

		public void Start()
		{
			if (mesh == null)
				mesh = GetComponent<MeshRenderer>();

			Vector3 size = Vector3.one;

			if (GetComponentInParent<Collider>() != null)
			{
				size = GetComponentInParent<Collider>().bounds.extents;
			}

			transform.localScale = new Vector3(size.x, Mathf.Min(size.x, size.z), size.z) * scaleMult;

			BoxCollider col = gameObject.AddComponent<BoxCollider>();
			col.isTrigger = true;
			col.center = transform.localPosition;
			col.size = transform.localScale;

			if (type == MarkerType.SpawnPoint)
			{
				objName = GetComponentInParent<SpawnResource>().name;
			}
			else if (type == MarkerType.Plot)
			{
				objName = transform.root.name;
			}
		}

		public void Update()
		{
			if (DebugCommand.DebugMode != mesh.enabled)
			{
				mesh.enabled = DebugCommand.DebugMode;
			}
		}

		public void OnMouseEnter()
		{
			hover = true;
		}

		public void OnMouseExit()
		{
			hover = true;
		}

		public void OnGUI()
		{
			if (hover)
			{
				TextAnchor defAnc = GUI.skin.label.alignment;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;

				GUI.Label(new Rect(0, 10, Screen.width, 30), $"Looking at: {objName}");

				GUI.skin.label.alignment = defAnc;
			}
		}
	}
}
