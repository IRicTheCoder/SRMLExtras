using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Components
{
	/// <summary>
	/// A game object made for mods, it gets converted to an actual game object
	/// </summary>
	public class ModdedGameObject
	{
		private readonly string name = "Modded Game Object";

		private readonly List<Component> components = new List<Component>();

		public string Tag { get; } = string.Empty;
		public ModdedTransform Transform => (ModdedTransform)components[0];

		public ModdedGameObject(string name, params Component[] comps)
		{
			this.name = name;
			components.Add(new ModdedTransform().SetModdedObject(this));
			components.AddRange(comps);
		}

		public T GetComponent<T>() where T : Component
		{
			foreach (Component comp in components)
			{
				if (comp is T)
					return (T)comp;
			}

			return null;
		}

		public ModdedGameObject SetParent(ModdedGameObject parent)
		{
			Transform.parent = parent.Transform;
			return this;
		}

		public void SetComponentPrivateField<T>(string name, object value) where T : Component
		{
			T comp = GetComponent<T>();

			try
			{
				FieldInfo field = comp.GetType().GetField(name, BindingFlags.NonPublic);
				field.SetValue(comp, value);
			}
			catch { }
		}

		public GameObject ToGameObject(Transform parent)
		{
			GameObject obj = new GameObject(name);
			foreach (Component comp in components)
			{
				if (!(comp is ModdedTransform))
					obj.AddComponent(comp.GetType());
			}

			obj.transform.parent = parent;
			obj.transform.position = Transform.position;
			obj.transform.rotation = Transform.rotation;
			obj.transform.localScale = Transform.localScale;

			return obj;
		}
	}
}
