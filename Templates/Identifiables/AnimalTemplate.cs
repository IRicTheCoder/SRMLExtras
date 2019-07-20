using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace SRMLExtras.Templates
{
	public class AnimalTemplate : ModPrefab<AnimalTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Mesh mesh;
		protected Material[] materials;

		protected GameObject skinnedMesh;

		protected Component moveComponent;

		protected int minRepHours = 8;
		protected int maxRepHours = 16;

		protected Identifiable mate;
		protected GameObject child;
		protected GameObject elder;

		protected Animator animator;

		protected GameObject bones;

		protected bool isChild;
		protected int childHours;

		protected float eggPeriod;
		protected GameObject egg;

		public AnimalTemplate(string name, Identifiable.Id ID, Mesh mesh, Material[] materials, Animator animator, bool isChild = false) : base(name)
		{
			this.ID = ID;
			this.mesh = mesh;
			this.materials = materials;
			this.animator = animator;
			this.isChild = isChild;
			
			moveComponent = new ChickenRandomMove()
			{
				maxJump = isChild ? 0.7f : 1f,
				walkForwardForce = isChild ? 2.5f : 3.333f,
				flapCue = EffectObjects.flapCue
			};

			skinnedMesh = BaseObjects.originSkinnedMesh["HenSkinned"];
			bones = BaseObjects.originBones["HenBones"];
		}

		public AnimalTemplate SetReproduceObjects(Identifiable mate, GameObject child, GameObject elder)
		{
			this.mate = mate;
			this.child = child;
			this.elder = elder;
			return this;
		}

		public AnimalTemplate SetMoveComponent(Component comp)
		{
			moveComponent = comp;
			return this;
		}

		public AnimalTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public AnimalTemplate SetReproduceInfo(int minReproduceGameHours, int maxReproduceGameHours)
		{
			minRepHours = minReproduceGameHours;
			maxRepHours = maxReproduceGameHours;
			return this;
		}

		public AnimalTemplate SetBones(GameObject skinnedMesh, GameObject bones)
		{
			this.skinnedMesh = skinnedMesh;
			this.bones = bones;
			return this;
		}

		public AnimalTemplate SetChildInfo(int delayGameHours, float eggPeriod, GameObject egg)
		{
			childHours = delayGameHours;
			this.eggPeriod = eggPeriod;
			this.egg = egg;
			return this;
		}

		public override AnimalTemplate Create()
		{
			// Create main object
			mainObject.AddComponents(
				new Identifiable()
				{
					id = ID
				},
				new Vacuumable()
				{
					size = vacSize
				},
				new Create<Rigidbody>((body) =>
				{
					body.drag = 0.2f;
					body.angularDrag = 5f;
					body.mass = 0.3f;
					body.useGravity = true;
				}),
				new DragFloatReactor()
				{
					floatDragMultiplier = 10
				},
				new Create<BoxCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.size = Vector3.one * 0.5f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new Create<SECTR_PointSource>((source) =>
				{
					source.Loop = false;
					source.PlayOnStart = false;
					source.RestartLoopsOnEnabled = false;
					source.Volume = 1;
					source.Pitch = 1;
				}),
				new PlaySoundOnHit()
				{
					hitCue = isChild ? EffectObjects.hitChick : EffectObjects.hitChicken,
					minTimeBetween = 0.2f,
					minForce = 1,
					includeControllerCollisions = false
				},
				new SlimeSubbehaviourPlexer()
				{

				},
				moveComponent,
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.5f;
					col.isTrigger = true;
				}),
				new SlimeAudio()
				{
					slimeSounds = BaseObjects.originSounds["HenHen"]
				},
				new KeepUpright()
				{
					stability = 0.9f,
					speed = 2
				},
				new DestroyOnIgnite(),
				new AttachFashions()
				{
					gordoMode = false,
				}
			).AddAfterChildren(ConfigBones);

			if (mate != null)
			{
				mainObject.AddComponents(
					new Reproduce()
					{
						nearMateId = mate,
						maxDistToMate = 10f,
						densityDist = 10,
						maxDensity = 12,
						deluxeDensityFactor = 2,
						minReproduceGameHours = minRepHours,
						maxReproduceGameHours = maxRepHours,
						produceFX = EffectObjects.stars,
						childPrefab = child
					}
				);
			}

			if (isChild)
			{
				mainObject.AddComponents(
					new TransformAfterTime()
					{
						delayGameHours = childHours,
						transformFX = EffectObjects.stars,
					},
					new EggActivator()
					{
						eggPeriod = eggPeriod,
						activateObj = egg
					}
				);
			}
			else
			{
				mainObject.AddComponents(
					new TransformChanceOnReproduce()
					{
						transformChance = 0.05f,
						targetPrefab = elder,
						transformFX = EffectObjects.stars
					}
				);
			}

			// Create body
			mainObject.AddChild(new GameObjectTemplate("Body",
				new Create<MeshFilter>((filter) => filter.sharedMesh = mesh),
				new Create<MeshRenderer>((render) => render.sharedMaterials = materials)
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 0.5f));

			// Create delaunch trigger
			mainObject.AddChild(new GameObjectTemplate("DelaunchTrigger",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new VacDelaunchTrigger()
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 1.4f));

			// Create animal
			mainObject.AddChild(new GameObjectTemplate("Animal",
				animator,
				new Create<LODGroup>((lod) =>
				{
					lod.localReferencePoint = new Vector3(0, 0.5f, -0.1f);
					lod.size = 1.893546f;
				})
			).SetTransform(Vector3.up * -0.5f, Vector3.zero, Vector3.one));

			return this;
		}

		internal void ConfigBones(GameObject obj)
		{
			// Setup Bones
			GameObject sm = skinnedMesh.CreatePrefabCopy();
			sm.name = "mesh_body";
			sm.transform.parent = obj.transform;

			GameObject b = bones.CreatePrefabCopy();
			b.name = "root";
			b.transform.parent = obj.transform;

			// Read all bones to the skinned mesh
			SkinnedMeshRenderer render = sm.GetComponent<SkinnedMeshRenderer>();
			render.rootBone = b.FindChild("bone_spine").transform;

			List<Transform> addBones = new List<Transform>();
			foreach (GameObject bone in b.FindChildrenWithPartialName("bone_"))
				addBones.Add(bone.transform);

			render.bones = addBones.ToArray();

			// Add bones to other components
			obj.GetComponent<AttachFashions>().attachmentFront = obj.FindChild("bone_attachment_front").transform;
		}
	}
}
