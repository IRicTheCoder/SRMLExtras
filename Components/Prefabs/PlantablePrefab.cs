using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRMLExtras.Components.Prefabs
{
	/// <summary>
	/// A modded version of a plantable prefab
	/// </summary>
	public class PlantablePrefab : ModdedPrefab
	{
		private string patchName = "patchModded01";
		private bool isDeluxe = false;

		/*
		THINGS TO LOOK AT:
		- LookupDirector GetResourcePrefab
		
		THIS IS THE BASE OF THE PLANTABLE PREFAB
		patchCarrot01
		C: Transform
		C: SpawnResource
		C: BoxCollider
		C: ScaleYOnlyMarker

		THEN 20 OF THIS AS CHILDREN (more in case of deluxe)
		SpawnJoint01
		C: Transform
		C: MeshFilter
		C: MeshRenderer
		C: Rigidbody
		C: FixedJoint
		C: HideOnStart

		THIS IS THE SPAWN RESOURCE PREFAB (Make it's own prefab)
		S:veggieCarrot
		S:C: Transform
		S:C: MeshFilter
		S:C: CapsuleCollider
		S:C: Rigidbody
		S:C: Vacuumable
		S:C: Identifiable
		S:C: ResourceCycle
		S:C: SECTR_PointSource
		S:C: BuoyancyOffset
		S:C: DragFloatReactor
		S:C: PlaySoundOnHit
		S:C: CollisionAggregator
		S:C: SphereCollider
		S:C: RegionMember
		 S:DelaunchTrigger
		 S:C: Transform
		 S:C: SphereCollider
		 S:C: VacDelaunchTrigger
		 S:model_carrot
		 S:C: Transform
		 S:C: MeshRenderer
		 S:C: MeshFilter
		 S:Shadow
		 S:C: Transform
		 S:C: MeshFilter
		 S:C: MeshRenderer
		*/

		public PlantablePrefab(string patchName)
		{
			this.patchName = patchName;
		}

		public override void Setup()
		{
			mainObject = new ModdedGameObject(patchName,
				new SpawnResource()
				{
					
				},
				new UnityEngine.BoxCollider()
				{
					
				},
				new ScaleYOnlyMarker()
				{
					
				}
			);
		}
	}
}
