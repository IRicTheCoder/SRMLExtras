using System.Reflection;
using UnityEngine;

namespace SRMLExtras.Prefabs
{
	public static class ComponentExtensions
	{
		public static T SetPrivateField<T>(this T comp, string name, object value) where T : Component
		{
			try
			{
				FieldInfo field = comp.GetType().GetField(name, BindingFlags.NonPublic);
				field.SetValue(comp, value);
			}
			catch { }

			return comp;
		}


	}
}
