using System.Collections.Generic;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SRMLExtras.Templates
{
	public static class EffectObjects
	{
		// Identifiable Effects
		public static GameObject fxCrateBrake;
		public static GameObject fxRottenDespawn;
		public static GameObject fxStars;

		public static GameObject fxFashionApply;
		public static GameObject fxFashionBurst;

		public static GameObject fxWaterSplat;
		public static GameObject fxSplat;

		public static GameObject fxPlortDespawn;
		public static GameObject fxExplosion;

		public static GameObject fxSlimeEat;
		public static GameObject fxSlimeFavEat;
		public static GameObject fxSlimeTrans;
		public static GameObject fxSlimeProduce;
		public static GameObject fxFeralAura;

		public static GameObject fxGordoEat;
		public static GameObject fxGordoSplat;

		// Audio Cues
		public static SECTR_AudioCue cueFruitRelease;
		public static SECTR_AudioCue cueVeggieRelease;

		public static SECTR_AudioCue cueHitFruit;
		public static SECTR_AudioCue cueHitVeggie;
		public static SECTR_AudioCue cueHitChicken;
		public static SECTR_AudioCue cueHitChick;
		public static SECTR_AudioCue cueHitPlort;

		public static SECTR_AudioCue cueFlap;

		public static SECTR_AudioCue cueGordoGlup;
		public static SECTR_AudioCue cueGordoStrain;
		public static SECTR_AudioCue cueGordoBurst;

		// Audio Cue Instances
		public static SECTR_AudioCueInstance fruitCueInstance;

		// Populates required values
		internal static void Populate()
		{
			// Identifiable Effects
			fxCrateBrake = BaseObjects.originFXs["fxCrateBurst"];
			fxRottenDespawn = BaseObjects.originFXs["FX RottenDespawn"];
			fxStars = BaseObjects.originFXs["FX Stars"];

			fxFashionApply = BaseObjects.originFXs["FX FashionApply"];
			fxFashionBurst = BaseObjects.originFXs["FX FashionBurst"];

			fxWaterSplat = BaseObjects.originFXs["FX waterSplat"];
			fxSplat = BaseObjects.originFXs["FX Splat"];

			fxPlortDespawn = BaseObjects.originFXs["FX PlortDespawn"];
			fxExplosion = BaseObjects.originFXs["FX Explosion"];

			fxSlimeEat = BaseObjects.originFXs["FX Chomp"];
			fxSlimeFavEat = BaseObjects.originFXs["FX slimeEatFav"];
			fxSlimeProduce = BaseObjects.originFXs["FX PlortProduced"];
			fxSlimeTrans = BaseObjects.originFXs["FX Largo Transform"];
			fxFeralAura = BaseObjects.originFXs["FX auraFeral"];

			fxGordoEat = BaseObjects.originFXs["FX Gordo Eat"];
			fxGordoSplat = BaseObjects.originFXs["FX Gordo Splat"];

			// Audio Cues
			/*fruitRelease = BaseObjects.originCues["Fruit Rustle"];
			veggieRelease = BaseObjects.originCues["Veggie Rustle"];
			hitFruit = BaseObjects.originCues["HitFruit"];
			hitVeggie = BaseObjects.originCues["HitVeggie"];
			hitChicken = BaseObjects.originCues["HitChicken"];
			hitChick = BaseObjects.originCues["HitChick"];
			hitPlort = BaseObjects.originCues["HitPlort"];
			flapCue = BaseObjects.originCues["ChickenFlap"];*/

			// Audio Cue Instances
			// TODO: Check this
			//fruitCueInstance = Director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT).GetComponent<SECTR_PointSource>().GetPrivateField<SECTR_AudioCueInstance>("instance");
		}

		internal static void LatePopulate()
		{
		}
	}
}
