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
		// The Director
		private static LookupDirector Director => GameContext.Instance.LookupDirector;

		// Identifiable Effects
		public static GameObject crateBrake;
		public static GameObject rottenDespawn;
		public static GameObject stars;
		public static GameObject fashionApply;
		public static GameObject fashionBurst;
		public static GameObject waterSplat;
		public static GameObject plortDespawn;
		public static GameObject explosion;

		// Audio Cues
		public static SECTR_AudioCue fruitRelease;
		public static SECTR_AudioCue veggieRelease;
		public static SECTR_AudioCue hitFruit;
		public static SECTR_AudioCue hitVeggie;
		public static SECTR_AudioCue hitChicken;
		public static SECTR_AudioCue hitChick;
		public static SECTR_AudioCue hitPlort;
		public static SECTR_AudioCue flapCue;

		// Audio Cue Instances
		public static SECTR_AudioCueInstance fruitCueInstance;

		// Populates required values
		internal static void Populate()
		{
			// Identifiable Effects
			crateBrake = BaseObjects.originFXs["fxCrateBurst"];
			rottenDespawn = BaseObjects.originFXs["FX RottenDespawn"];
			stars = BaseObjects.originFXs["FX Stars"];
			fashionApply = BaseObjects.originFXs["FX FashionApply"];
			fashionBurst = BaseObjects.originFXs["FX FashionBurst"];
			waterSplat = BaseObjects.originFXs["FX waterSplat"];
			plortDespawn = BaseObjects.originFXs["FX PlortDespawn"];
			explosion = BaseObjects.originFXs["FX Explosion"];

			// Audio Cues
			fruitRelease = BaseObjects.originCues["Fruit Rustle"];
			veggieRelease = BaseObjects.originCues["Veggie Rustle"];
			hitFruit = BaseObjects.originCues["HitFruit"];
			hitVeggie = BaseObjects.originCues["HitVeggie"];
			hitChicken = BaseObjects.originCues["HitChicken"];
			hitChick = BaseObjects.originCues["HitChick"];
			hitPlort = BaseObjects.originCues["HitPlort"];
			flapCue = BaseObjects.originCues["ChickenFlap"];

			// Audio Cue Instances
			// TODO: Check this
			//fruitCueInstance = Director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT).GetComponent<SECTR_PointSource>().GetPrivateField<SECTR_AudioCueInstance>("instance");
		}

		internal static void LatePopulate()
		{
		}
	}
}
