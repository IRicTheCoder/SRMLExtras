using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class ActionOnAwake : MonoBehaviour
	{
		public List<string> actions;

		public void Awake()
		{
			foreach (string action in actions)
				TemplateActions.RunAction(action, gameObject);
		}
	}
}
