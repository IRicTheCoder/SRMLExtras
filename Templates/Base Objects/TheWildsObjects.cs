using System.Collections.Generic;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using SRML.Console;
using SRMLExtras.Markers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SRMLExtras.Templates
{
	public static class TheWildsObjects
	{
		// The Director
		private static LookupDirector Director => GameContext.Instance.LookupDirector;
		private static GameModel GameModel => SceneContext.Instance.GameModel;

		// Kookadoba Plant
		public static Mesh kookadobaPlant;
		public static Material[] kookadobaPlantMaterials;

		// Populates required values
		internal static void Populate()
		{
			
		}

		internal static void LatePopulate()
		{
			// Loads kookadoba plant
			kookadobaPlant = BaseObjects.originMesh["desert_tree_top"];
			kookadobaPlantMaterials = BaseObjects.originMaterial["objKookadobaTreeLeaves01"].Group();
		}
	}
}
