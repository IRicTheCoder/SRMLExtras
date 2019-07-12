using System.Collections.Generic;
using UnityEngine;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// A game object made for mods, it gets converted to an actual game object
	/// </summary>
	public class ModdedGameObject : IChild
	{
		private readonly List<System.Type> components = new List<System.Type>();

		public readonly List<IChild> children = new List<IChild>();

		public string Name { get; set; } = "Modded Game Object";
		public string Tag { get; set; } = "Untagged";
		public LayerMask Layer { get; set; } = LayerMask.NameToLayer("Default");

		public ModdedGameObject(string name, params System.Type[] comps)
		{
			Name = name;
			components.AddRange(comps);
		}

		public void AddChild(IChild gameObject)
		{
			children.Add(gameObject);
		}

		public System.Type[] GetComponents()
		{
			return components.ToArray();
		}

		public GameObject ToGameObject(Transform parent)
		{
			GameObject obj = new GameObject(Name);
			foreach (System.Type comp in components)
			{
				obj.AddComponent(comp.GetType());
			}
			obj.tag = Tag;
			obj.layer = Layer;

			obj.transform.parent = parent;

			return obj;
		}
	}
}
