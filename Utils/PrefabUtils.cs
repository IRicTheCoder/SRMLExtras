using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;

namespace SRMLExtras
{
	public static class PrefabUtils
	{
		public static void DumpPrefab(GameObject obj, string folder = null)
		{
			try
			{
				List<GameObject> subPrefabs = DumpPrefabStructure(obj, (folder != null ? folder + "/" : string.Empty) + obj.name + "/");
				List<GameObject> prefabs = new List<GameObject>(subPrefabs);

				while (prefabs.Count > 0)
				{
					subPrefabs.Clear();
					foreach (GameObject prefab in prefabs)
					{
						if (prefab != null)
							subPrefabs.AddRange(DumpPrefabStructure(prefab, (folder != null ? folder + "/" : string.Empty) + obj.name + "/"));
					}

					prefabs.Clear();
					prefabs.AddRange(subPrefabs);
				}
			}
			catch { }
			finally { }
		}

		private static List<GameObject> DumpPrefabStructure(GameObject go, string folder = "")
		{
			List<GameObject> subPrefabs = new List<GameObject>();

			void RunThroughChildren(GameObject parent, StreamWriter writer, string indent)
			{
				writer.WriteLine($"{indent}{parent.name.ToUpper()} ({parent.name})");

				foreach (Component comp in parent.GetComponents<Component>())
				{
					writer.WriteLine($"{indent}C: {comp.GetType().Name}");

					foreach (FieldInfo field in comp.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
					{
						if (field.FieldType == typeof(GameObject) && !field.FieldType.HasElementType && !field.Name.Equals("gameObject"))
						{
							GameObject value = field.GetValue(comp) as GameObject;
							writer.WriteLine($"{indent}f - ({field.Name} - go: {value?.name})");
							if (value != null) subPrefabs.Add(value);
						}
						else if (field.FieldType.HasElementType && field.FieldType.GetElementType() == typeof(GameObject))
						{
							writer.WriteLine($"{indent}f - ({field.Name} - {field.GetValue(comp)?.ToString().Replace("\n", "\t")})");

							GameObject[] value = field.GetValue(comp) as GameObject[];
							foreach (GameObject child in value)
							{
								writer.WriteLine($"{indent}  go: {child?.name}");
								if (child != null) subPrefabs.Add(child);
							}
						}
						else
							writer.WriteLine($"{indent}f - ({field.Name} - {field.GetValue(comp)?.ToString().Replace("\n", "\t")})");
					}

					foreach (FieldInfo field in comp.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
					{
						if (field.FieldType == typeof(GameObject) && !field.FieldType.HasElementType && !field.Name.Equals("gameObject"))
						{
							GameObject value = field.GetValue(comp) as GameObject;
							writer.WriteLine($"{indent}f - ({field.Name} - go: {value?.name})");
							if (value != null) subPrefabs.Add(value);
						}
						else if (field.FieldType.HasElementType && field.FieldType.GetElementType() == typeof(GameObject))
						{
							writer.WriteLine($"{indent}f - ({field.Name} - {field.GetValue(comp)?.ToString().Replace("\n", "\t")})");

							GameObject[] value = field.GetValue(comp) as GameObject[];
							foreach (GameObject child in value)
							{
								writer.WriteLine($"{indent}  go: {child?.name}");
								if (child != null) subPrefabs.Add(child);
							}
						}
						else
							writer.WriteLine($"{indent}f - [{field.Name} - {field.GetValue(comp)?.ToString().Replace("\n", "\t")}]");
					}

					foreach (PropertyInfo field in comp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
					{
						if (field.PropertyType == typeof(GameObject) && !field.PropertyType.HasElementType && !field.Name.Equals("gameObject"))
						{
							GameObject value = field.GetValue(comp, null) as GameObject;
							writer.WriteLine($"{indent}p - ({field.Name} - go: {value?.name})");
							if (value != null) subPrefabs.Add(value);
						}
						else if (field.PropertyType.HasElementType && field.PropertyType.GetElementType() == typeof(GameObject))
						{
							writer.WriteLine($"{indent}p - ({field.Name} - {field.GetValue(comp, null)?.ToString().Replace("\n", "\t")})");

							GameObject[] value = field.GetValue(comp, null) as GameObject[];
							foreach (GameObject child in value)
							{
								writer.WriteLine($"{indent}  go: {child?.name}");
								if (child != null) subPrefabs.Add(child);
							}
						}
						else
							writer.WriteLine($"{indent}p - ({field.Name} - {field.GetValue(comp, null)?.ToString().Replace("\n", "\t")})");
					}

					foreach (PropertyInfo field in comp.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
					{
						if (field.PropertyType == typeof(GameObject) && !field.PropertyType.HasElementType && !field.Name.Equals("gameObject"))
						{
							GameObject value = field.GetValue(comp, null) as GameObject;
							writer.WriteLine($"{indent}p - ({field.Name} - go: {value?.name})");
							if (value != null) subPrefabs.Add(value);
						}
						else if (field.PropertyType.HasElementType && field.PropertyType.GetElementType() == typeof(GameObject))
						{
							writer.WriteLine($"{indent}p - ({field.Name} - {field.GetValue(comp, null)?.ToString().Replace("\n", "\t")})");

							GameObject[] value = field.GetValue(comp, null) as GameObject[];
							foreach (GameObject child in value)
							{
								writer.WriteLine($"{indent}  go: {child?.name}");
								if (child != null) subPrefabs.Add(child);
							}
						}
						else
							writer.WriteLine($"{indent}p - [{field.Name} - {field.GetValue(comp, null)?.ToString().Replace("\n", "\t")}]");
					}
				}

				foreach (Transform child in parent.transform)
				{ 					
					RunThroughChildren(child.gameObject, writer, "  " + indent);
				}
			}

			string path = Path.Combine(Application.dataPath, "../_Prefabs/" + folder + go.name + ".txt");

			if (!Directory.Exists(Path.GetDirectoryName(path)))
				Directory.CreateDirectory(Path.GetDirectoryName(path));

			if (!File.Exists(path))
			{
				using (StreamWriter writer = File.CreateText(path))
				{
					RunThroughChildren(go, writer, string.Empty);
				}
			}

			return subPrefabs;
		}
	}
}
