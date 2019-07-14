using UnityEngine;

namespace SRMLExtras.Templates
{
	public class Create<T> : Component, ICreateComponent where T : Component
	{
		private System.Action<T> action;

		public Create(System.Action<T> action)
		{
			this.action = action;
		}

		public void AddComponent(GameObject obj)
		{
			action(obj.AddComponent<T>());
		}
	}
}
