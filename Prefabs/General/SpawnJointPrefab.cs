using UnityEngine;

namespace SRMLExtras.Prefabs
{
	public class SpawnJointPrefab : ModdedPrefab
	{
		private readonly string jointName = "spawnJoint01";

		private Vector3 position = Vector3.zero;
		private Vector3 rotation = Vector3.zero;
		private Vector3 scale = Vector3.one;

		private readonly Mesh customMesh;
		private readonly Material[] customMaterials;

		private readonly Identifiable.Id ID;
		
		public SpawnJointPrefab(string jointName, Vector3 position, Vector3 rotation, Vector3 scale, Mesh customMesh = null, Material[] customMaterials = null) : base(jointName)
		{
			this.jointName = jointName;
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
			this.customMesh = customMesh;
			this.customMaterials = customMaterials;
			ID = Identifiable.Id.NONE;
		}

		public SpawnJointPrefab(string jointName, Vector3 position, Vector3 rotation, Vector3 scale, Identifiable.Id ID) : base(jointName)
		{
			this.jointName = jointName;
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
			this.ID = ID;

			Create();
		}

		public override ModdedPrefab Create()
		{
			MainObject = new ModdedGameObject(jointName, typeof(MeshFilter),
				typeof(MeshRenderer), typeof(Rigidbody), typeof(FixedJoint), 
				typeof(HideOnStart));

			return this;
		}

		public override void Setup(GameObject root)
		{
			// Setup Transform
			root.transform.position = position;
			root.transform.eulerAngles = rotation;
			root.transform.localScale = scale;

			// Setup Render Stuff
			root.GetComponent<MeshFilter>().sharedMesh = ID == Identifiable.Id.NONE ? customMesh : GameContext.Instance.LookupDirector.GetPrefab(ID).GetComponent<MeshFilter>().sharedMesh;
			root.GetComponent<MeshRenderer>().sharedMaterials = ID == Identifiable.Id.NONE ? customMaterials : GameContext.Instance.LookupDirector.GetPrefab(ID).GetComponent<MeshRenderer>().sharedMaterials;

			// Setup RigidBody
			Rigidbody body = root.GetComponent<Rigidbody>();
			body.drag = 0;
			body.angularDrag = 0.05f;
			body.mass = 1;
			body.useGravity = false;
			body.isKinematic = true;

			// Setup Fixed Joint
			FixedJoint joint = root.GetComponent<FixedJoint>();
			joint.axis = Vector3.right;
			joint.autoConfigureConnectedAnchor = true;
			joint.enableCollision = false;
			joint.enablePreprocessing = true;
			joint.massScale = 1;
			joint.connectedMassScale = 1;
		}
	}
}
