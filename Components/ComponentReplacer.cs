using System;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace SRMLExtras.Components
{
	public static class ComponentReplacer
	{
		internal static Dictionary<Type, Type> uiReplacements = new Dictionary<Type, Type>()
		{
			{
				typeof(GardenUI),
				typeof(Plots.ModdedGardenUI)
			}
		};

		internal static Dictionary<Type, Type> replacements = new Dictionary<Type, Type>()
		{
			{
				typeof(GardenCatcher),
				typeof(Plots.ModdedGardenCatcher)
			}
		};

		// PATCHES THE COMPONENTS
		/*[HarmonyPatch(typeof(SRBehaviour))]
		internal static class SRBehaviourPatcher
		{
			public static MethodBase TargetMethod(Harmony instance)
			{
				return typeof(SRBehaviour).GetConstructor(Type.EmptyTypes);
			}

			public static void Postfix(SRBehaviour __instance)
			{
				if (__instance is BaseUI)
					return;

				if (replacements.ContainsKey(__instance.GetType()))
				{
					Component comp = __instance.gameObject.AddComponent(replacements[__instance.GetType()]);
					((IModdedComponent)comp).LoadFromOriginal(__instance);

					UnityEngine.Object.Destroy(__instance);
				}
			}
		}*/

		[HarmonyPatch(typeof(BaseUI))]
		[HarmonyPatch("Awake")]
		internal static class BaseUIPatcher
		{
			public static bool Prefix(BaseUI __instance)
			{
				if (__instance is IModdedComponent)
					return true;

				if (uiReplacements.ContainsKey(__instance.GetType()))
				{
					BaseUI comp = __instance.gameObject.AddComponent(uiReplacements[__instance.GetType()]) as BaseUI;
					((IModdedComponent)comp).SetOriginal(__instance);

					/*if (comp is LandPlotUI)
						((LandPlotUI)comp).RebuildUI();*/

					UnityEngine.Object.Destroy(__instance);
					return false;
				}

				return true;
			}
		}
	}
}
