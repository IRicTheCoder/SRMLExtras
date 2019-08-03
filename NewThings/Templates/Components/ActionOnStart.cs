using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class ActionOnStart : MonoBehaviour
	{
		public List<string> actions;

		public void Start()
		{
			foreach (string action in actions)
				TemplateActions.RunAction(action, gameObject);
		}
	}
}
