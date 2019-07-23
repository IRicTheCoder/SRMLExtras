using System;
using System.Collections.Generic;
using SRMLExtras;
using UnityEngine;

public static class LookupDirectorExtension
{
	// Garden Resources
	internal readonly static Dictionary<Identifiable.Id, GameObject> gardenResources = new Dictionary<Identifiable.Id, GameObject>();

	// Get Prefab Methods
	public static GameObject GetGardenResourcePrefab(this LookupDirector director, Identifiable.Id ID)
	{
		return gardenResources.ContainsKey(ID) ? gardenResources[ID] : null;
	}

	public static GameObject GetLargoPrefab(this LookupDirector director, Identifiable.Id slimeA, Identifiable.Id slimeB)
	{
		Identifiable.Id largo = Identifiable.Id.NONE;

		string nameA = slimeA.ToString().Replace("_SLIME", "");
		string nameB = slimeB.ToString().Replace("_SLIME", "");

		string largoA = $"{nameA}_{nameB}_LARGO";
		string largoB = $"{nameB}_{nameA}_LARGO";

		object result = Enum.Parse(typeof(Identifiable.Id), largoA) ?? Enum.Parse(typeof(Identifiable.Id), largoB);

		if (result != null)
			largo = (Identifiable.Id)result;

		return largo == Identifiable.Id.NONE ? null : director.GetPrefab(largo);
	}

	// Get Component Methods
	public static Identifiable GetIdentifiable(this LookupDirector director, Identifiable.Id ID)
	{
		return director.GetPrefab(ID).GetComponent<Identifiable>();
	}

	// Check Methods
	public static bool LargoExists(this LookupDirector director, Identifiable.Id slimeA, Identifiable.Id slimeB)
	{
		string nameA = slimeA.ToString().Replace("_SLIME", "");
		string nameB = slimeB.ToString().Replace("_SLIME", "");

		string largoA = $"{nameA}_{nameB}_LARGO";
		string largoB = $"{nameB}_{nameA}_LARGO";

		object result = Enum.Parse(typeof(Identifiable.Id), largoA) ?? Enum.Parse(typeof(Identifiable.Id), largoB);

		return result != null;
	}

	// Initializes the Extension
	internal static void InitExtension(this LookupDirector director)
	{
		// Required prefabs
		GameObject gingerSpawnResource = director.GetPrefab(Identifiable.Id.GINGER_VEGGIE).CreatePrefabCopy();
		gingerSpawnResource.GetComponent<ResourceCycle>().unripeGameHours = 12;

		GameObject kookadobaSpawnResource = director.GetPrefab(Identifiable.Id.KOOKADOBA_FRUIT).CreatePrefabCopy();
		kookadobaSpawnResource.GetComponent<ResourceCycle>().unripeGameHours = 6;

		// Load garden resources
		gardenResources.Add(Identifiable.Id.BEET_VEGGIE, director.GetPrefab(Identifiable.Id.BEET_VEGGIE));
		gardenResources.Add(Identifiable.Id.CARROT_VEGGIE, director.GetPrefab(Identifiable.Id.CARROT_VEGGIE));
		gardenResources.Add(Identifiable.Id.GINGER_VEGGIE, gingerSpawnResource);
		gardenResources.Add(Identifiable.Id.OCAOCA_VEGGIE, director.GetPrefab(Identifiable.Id.OCAOCA_VEGGIE));
		gardenResources.Add(Identifiable.Id.ONION_VEGGIE, director.GetPrefab(Identifiable.Id.BEET_VEGGIE));
		gardenResources.Add(Identifiable.Id.PARSNIP_VEGGIE, director.GetPrefab(Identifiable.Id.PARSNIP_VEGGIE));
		gardenResources.Add(Identifiable.Id.CUBERRY_FRUIT, director.GetPrefab(Identifiable.Id.CUBERRY_FRUIT));
		gardenResources.Add(Identifiable.Id.KOOKADOBA_FRUIT, kookadobaSpawnResource);
		gardenResources.Add(Identifiable.Id.LEMON_FRUIT, director.GetPrefab(Identifiable.Id.LEMON_FRUIT));
		gardenResources.Add(Identifiable.Id.MANGO_FRUIT, director.GetPrefab(Identifiable.Id.MANGO_FRUIT));
		gardenResources.Add(Identifiable.Id.PEAR_FRUIT, director.GetPrefab(Identifiable.Id.PEAR_FRUIT));
		gardenResources.Add(Identifiable.Id.POGO_FRUIT, director.GetPrefab(Identifiable.Id.POGO_FRUIT));
	}
}
