using System.Reflection;
using UnityEngine;

namespace SRMLExtras
{
	public static class GameObjectExtensions
	{
		public static GameObject FindChildWithPartialName(this GameObject obj, string name)
		{
			GameObject result = null;

			foreach (Transform child in obj.transform)
			{
				if (child.name.StartsWith(name))
				{
					result = child.gameObject;
					break;
				}

				if (child.childCount > 0)
				{
					result = child.gameObject.FindChildWithPartialName(name);
					if (result != null)
						break;
				}
			}

			return result;
		}


	}
}
