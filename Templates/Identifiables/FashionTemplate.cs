﻿using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;
using UnityEngine.UI;

namespace SRMLExtras.Templates
{
	// TODO: To Finish
	public class FashionTemplate : ModPrefab<FashionTemplate>
	{
		protected Identifiable.Id ID;

		protected Vacuumable.Size vacSize = Vacuumable.Size.NORMAL;

		protected Fashion.Slot slot;
		protected GameObject attachPrefab;

		protected Sprite icon;

		public FashionTemplate(string name, Identifiable.Id ID, GameObject attachPrefab, Sprite icon, Fashion.Slot slot = Fashion.Slot.FRONT) : base(name)
		{
			this.ID = ID;
			this.attachPrefab = attachPrefab;
			this.icon = icon;
			this.slot = slot;
		}

		public FashionTemplate SetVacSize(Vacuumable.Size vacSize)
		{
			this.vacSize = vacSize;
			return this;
		}

		public override FashionTemplate Create()
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
					body.drag = 0;
					body.angularDrag = 0.05f;
					body.mass = 0.1f;
					body.useGravity = true;
				}),
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.5f;
				}),
				new CollisionAggregator(),
				new RegionMember()
				{
					canHibernate = true
				},
				new Fashion()
				{
					slot = slot,
					attachPrefab = attachPrefab,
					attachFX = EffectObjects.fashionApply
				},
				new DestroyOnTouching()
				{
					hoursOfContactAllowed = 0,
					wateringRadius = 0,
					wateringUnits = 0,
					destroyFX = EffectObjects.fashionBurst,
					touchingWaterOkay = false,
					touchingAshOkay = false,
					reactToActors = false,
					liquidType = Identifiable.Id.WATER_LIQUID
				}
			).SetTransform(Vector3.zero, Vector3.zero, Vector3.one * 0.7f);

			// Create Icon UI content
			GameObjectTemplate imageBack = new GameObjectTemplate("Image Back",
				new Create<RectTransform>((trans) =>
				{
					trans.anchorMin = Vector2.zero;
					trans.anchorMax = Vector2.one;
					trans.anchoredPosition = Vector2.zero;
					trans.sizeDelta = Vector2.zero;
					trans.pivot = Vector2.one * 0.5f;
					trans.localEulerAngles = new Vector3(0, 180, 0);
				}),
				new CanvasRenderer(),
				new Create<Image>((img) =>
				{
					img.sprite = icon;
					img.overrideSprite = icon;
					img.type = Image.Type.Simple;
					img.preserveAspect = true;
					img.fillCenter = true;
					img.fillMethod = Image.FillMethod.Radial360;
					img.fillAmount = 1;
					img.fillClockwise = true;
					img.fillOrigin = 0;

					img.material = BaseObjects.originMaterial["Digital Icon Medium"];
					img.SetPrivateProperty("preferredWidth", 1024);
					img.SetPrivateProperty("preferredHeight", 1024);
				})
			);

			GameObjectTemplate image = new GameObjectTemplate("Image",
				new Create<RectTransform>((trans) =>
				{
					trans.anchorMin = Vector2.zero;
					trans.anchorMax = Vector2.one;
					trans.anchoredPosition = Vector2.zero;
					trans.sizeDelta = Vector2.zero;
					trans.pivot = Vector2.one * 0.5f;
					trans.localEulerAngles = Vector3.zero;
				}),
				new CanvasRenderer(),
				new Create<Image>((img) =>
				{
					img.sprite = icon;
					img.overrideSprite = icon;
					img.type = Image.Type.Simple;
					img.preserveAspect = true;
					img.fillCenter = true;
					img.fillMethod = Image.FillMethod.Radial360;
					img.fillAmount = 1;
					img.fillClockwise = true;
					img.fillOrigin = 0;

					img.material = BaseObjects.originMaterial["Digital Icon Medium"];
					img.SetPrivateProperty("preferredWidth", 1024);
					img.SetPrivateProperty("preferredHeight", 1024);
				})
			);

			GameObjectTemplate iconUI = new GameObjectTemplate("IconUI",
				new Create<RectTransform>((trans) =>
				{
					trans.anchorMin = Vector2.zero;
					trans.anchorMax = Vector2.zero;
					trans.anchoredPosition = Vector2.zero;
					trans.sizeDelta = Vector2.one * 80;
					trans.pivot = Vector2.one * 0.5f;
					trans.localEulerAngles = new Vector3(0, 180, 0);
					trans.offsetMin = Vector2.one * -40f;
					trans.offsetMax = Vector2.one * 40f;
					trans.localScale = new Vector3(0, 0, 1.3f);
				}),
				new Create<Canvas>((canvas) => canvas.renderMode = RenderMode.WorldSpace),
				new Create<CanvasScaler>((scale) =>
				{
					scale.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
					scale.referenceResolution = new Vector2(800, 600);
					scale.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
				})
			).AddChild(image).AddChild(imageBack);

			// Create Icon Pivot
			mainObject.AddChild(new GameObjectTemplate("Icon Pivot",
				new CameraFacingBillboard()
			).AddChild(iconUI));

			// Create Surround Sphere
			mainObject.AddChild(new GameObjectTemplate("SurroundSphere",
				new Create<MeshFilter>((filter) => filter.sharedMesh = BaseObjects.originMesh["plort_shell"]),
				new Create<MeshRenderer>((render) => render.sharedMaterials = BaseObjects.originMaterial["EyeShine"].Group())
			).SetTransform(Vector3.zero, new Vector3(45, 90, 0), Vector3.one * 0.5f));

			// Create delaunch
			mainObject.AddChild(new GameObjectTemplate("DelaunchTrigger",
				new Create<SphereCollider>((col) =>
				{
					col.center = Vector3.zero;
					col.radius = 0.1f;
					col.isTrigger = true;
				}),
				new VacDelaunchTrigger()
			));

			return this;
		}
	}
}
