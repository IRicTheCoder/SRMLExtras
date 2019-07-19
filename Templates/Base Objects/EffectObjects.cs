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
		public static GameObject reproduce;

		// Audio Cues
		public static SECTR_AudioCue releaseCue;
		public static SECTR_AudioCue hitFruit;
		public static SECTR_AudioCue flapCue;

		// Audio Cue Instances
		public static SECTR_AudioCueInstance fruitCueInstance;

		// Populates required values
		internal static void Populate()
		{
			// TODO: Remake this to use BaseObjects

			// Identifiable Effects
			crateBrake = BaseObjects.originFXs["fxCrateBurst"];
			rottenDespawn = BaseObjects.originFXs["FX RottenDespawn"];
			reproduce = BaseObjects.originFXs["FX Stars"];

			// Audio Cues
			releaseCue = Director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT).GetComponent<ResourceCycle>().releaseCue;
			hitFruit = Director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT).GetComponent<PlaySoundOnHit>().hitCue;
			flapCue = Director.GetPrefab(Identifiable.Id.HEN).GetComponent<ChickenRandomMove>().flapCue;

			// Audio Cue Instances
			// TODO: Check this
			//fruitCueInstance = Director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT).GetComponent<SECTR_PointSource>().GetPrivateField<SECTR_AudioCueInstance>("instance");
		}

		internal static void LatePopulate()
		{
		}
	}
}
