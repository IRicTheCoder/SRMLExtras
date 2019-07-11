using UnityEngine;

namespace SRMLExtras.Prefabs
{
	/// <summary>
	/// A mod version of the Transform, to be used in ModdedGameObjects
	/// </summary>
	public class ModdedTransform : Transform
	{
		public ModdedGameObject ModdedObject { get; private set; }

		public ModdedTransform() : base() { }

		public ModdedTransform(Vector3 position, Vector3 rotation, Vector3 scale) : base()
		{
			this.position = position;
			this.rotation = Quaternion.Euler(rotation);
			localScale = scale;
		}

		public ModdedTransform SetModdedObject(ModdedGameObject obj)
		{
			ModdedObject = obj;
			return this;
		}
	}
}
