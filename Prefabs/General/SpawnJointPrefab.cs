using UnityEngine;

namespace SRMLExtras.Prefabs
{
	public class SpawnJointPrefab : ModdedPrefab
	{
		private string jointName = "spawnJoint01";
		private Vector3 position = Vector3.zero;
		private Vector3 rotation = Vector3.zero;
		private Vector3 scale = Vector3.one;
		
		public SpawnJointPrefab(string jointName, Vector3 position, Vector3 rotation, Vector3 scale)  : base()
		{
			this.jointName = jointName;
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;

			Create();
		}

		public override void Create()
		{

		}

		public override void Setup(GameObject root)
		{
			/*MainObject = new ModdedGameObject(jointName,
				new MeshFilter()
				{
					
				},
				new MeshRenderer()
				{
					
				},
				new Rigidbody()
				{
					
				},
				new FixedJoint()
				{
					
				},
				new HideOnStart()
			);

			MainObject.Transform.position = position;
			MainObject.Transform.eulerAngles = rotation;
			MainObject.Transform.localScale = scale;*/
		}
	}
}
