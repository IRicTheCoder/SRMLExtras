using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace SRMLExtras.Templates
{
	/*[HarmonyPatch(typeof(SlimeEatTrigger))]
	[HarmonyPatch("Awake")]
	public static class FixedSlimeEatTrigger
	{
		public static bool Prefix(SlimeEatTrigger __instance, ref TimeDirector ___timeDirector, ref SlimeEat ___eat, ref AttackPlayer ___attack)
		{
			___timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
			___eat = __instance.gameObject.FindComponentInParent<SlimeEat>();
			SlimeEat slimeEat = ___eat;
			slimeEat.onFinishChompSuccess = (SlimeEat.OnFinishChompSuccessDelegate)Delegate.Combine(slimeEat.onFinishChompSuccess, GetDelegate<SlimeEat.OnFinishChompSuccessDelegate>(__instance, "OnFinishChompSuccess"));
			___attack = __instance.gameObject.FindComponentInParent<AttackPlayer>();
			if (___attack != null)
			{
				AttackPlayer attackPlayer = ___attack;
				attackPlayer.onFinishChompSuccess = (AttackPlayer.OnFinishChompSuccessDelegate)Delegate.Combine(attackPlayer.onFinishChompSuccess, GetDelegate<AttackPlayer.OnFinishChompSuccessDelegate>(__instance, "OnFinishChompSuccess"));
			}

			return false;
		}

		public static T GetDelegate<T>(SlimeEatTrigger eat, string name) where T : Delegate
		{
			return (T)Delegate.CreateDelegate(typeof(T), eat, name);
		}
	}*/
}
