using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace SRML.Components
{
	public static class ComponentReplacer
	{
		internal static Dictionary<Type, Type> replacements = new Dictionary<Type, Type>()
		{
			{
				typeof(GardenUI),
				typeof(Plots.ModdedGardenUI)
			},
			{
				typeof(GardenCatcher),
				typeof(Plots.ModdedGardenCatcher)
			}
		};

		// PATCHES THE COMPONENTS
		[HarmonyPatch(typeof(SRBehaviour))]
		[HarmonyPatch("Awake")]
		internal static class SRBehaviourPatcher
		{
			public static void Postfix(SRBehaviour __instance)
			{
				if (replacements.ContainsKey(__instance.GetType()))
				{
					Component comp = __instance.gameObject.AddComponent(replacements[__instance.GetType()]);
					((IModdedComponent)comp).LoadFromOriginal(__instance);

					UnityEngine.Object.Destroy(__instance);
				}
			}
		}
	}
}
