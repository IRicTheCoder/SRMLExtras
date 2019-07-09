using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using SRMLExtras.Configs.Attributes;

namespace SRMLExtras.Configs
{
	internal static class ConfigHandler
	{
		private static Dictionary<FileID, ConfigFile> files = new Dictionary<FileID, ConfigFile>();

		private static Assembly[] cachedModDlls;

		internal static void Init(params Assembly[] modDlls)
		{
			SRML.Console.Reload += ReloadConfigs;
			cachedModDlls = modDlls;

			CopyFiles();
			ReadFiles();
			Populate();
			SaveFiles();
		}

		internal static void ReloadConfigs()
		{
			ReadFiles();
			Populate();
			files.Clear();
		}

		// COPIES THE DEFAULT FILES IF THEY EXIST
		internal static void CopyFiles()
		{
			foreach (Assembly modDll in cachedModDlls)
			{
				// THIS LINE ENSURES THAT LOCATION IS ALWAYS RIGHT. USING Assembly.Location DOESN'T ALWAYS PROVIDE THE RIGHT RESULT
				string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(modDll.CodeBase).Path)), "Configs");

				// SEARCHS ALL FILES IN THE MANIFEST AND CREATES THEM
				// FILES NEED TO BE STORED IN FOLDER "Resources/Configs"
				foreach (string fileWithNamespace in modDll.GetManifestResourceNames())
				{
					string file = fileWithNamespace.Substring(fileWithNamespace.IndexOf('.') + 1);
					if (!file.StartsWith("Resources.Configs.") || !file.EndsWith(".config"))
						continue;

					if (!Directory.Exists(configFolder))
						Directory.CreateDirectory(configFolder);

					string pathFile = file.Replace("Resources.Configs.", "").Replace(".config", "").Replace('.', '/') + ".config";

					if (File.Exists(Path.Combine(configFolder, pathFile)))
						continue;

					if (!Directory.Exists(Path.Combine(configFolder, Path.GetDirectoryName(pathFile))))
						Directory.CreateDirectory(Path.Combine(configFolder, Path.GetDirectoryName(pathFile)));

					using (Stream stream = modDll.GetManifestResourceStream(fileWithNamespace))
					{
						using (StreamReader reader = new StreamReader(stream))
						{
							File.WriteAllText(Path.Combine(configFolder, pathFile), reader.ReadToEnd());
						}
					}
				}
			}
		}

		// READS ALL CONFIG FILES FROM A GIVEN ASSEMBLY, THIS CACHES VALUES TO USE DURING POPULATE, THIS PREVENTS MOST OVERHEAD
		// MAKING SETTING VALUES THROUGH ATTRIBUTES SAFE, CAUSE NO OTHER INSTRUCTION WILL RUN DURING THIS PROCESS
		internal static void ReadFiles()
		{
			files.Clear();
			foreach (Assembly modDll in cachedModDlls)
			{
				string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(modDll.CodeBase).Path)), "Configs");
				foreach (string file in Directory.GetFiles(configFolder, "*", SearchOption.AllDirectories))
				{
					string fileRelative = file.Replace(configFolder + "\\", "").Replace("\\", "/");
					files.Add(new FileID(modDll, fileRelative), new ConfigFile(modDll, fileRelative).Load());
				}
			}
		}


		// POPULATES EVERY CLASS WITH THE CONFIG VALUES. THESE VALUES CAN STILL BE OBTAINED IN A CONVENTIONAL WAY THROUGH FILES
		internal static void Populate()
		{
			foreach (Assembly modDll in cachedModDlls)
			{
				foreach (Type type in modDll.GetTypes())
				{
					foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
					{
						object[] attributes = field.GetCustomAttributes(typeof(ConfigValueAttribute), false);
						ConfigValueAttribute configValue = attributes.Length > 0 ? attributes[0] as ConfigValueAttribute : null;

						attributes = field.GetCustomAttributes(typeof(ConfigDescriptionAttribute), false);
						ConfigDescriptionAttribute description = attributes.Length > 0 ? attributes[0] as ConfigDescriptionAttribute : null;

						attributes = field.GetCustomAttributes(typeof(ConfigCategoryAttribute), false);
						ConfigCategoryAttribute category = attributes.Length > 0 ? attributes[0] as ConfigCategoryAttribute : null;

						attributes = field.GetCustomAttributes(typeof(ConfigConverterAttribute), false);
						ConfigConverterAttribute converter = attributes.Length > 0 ? attributes[0] as ConfigConverterAttribute : null;

						attributes = field.GetCustomAttributes(typeof(ConfigUIAttribute), false);
						ConfigUIAttribute uiDesign = attributes.Length > 0 ? attributes[0] as ConfigUIAttribute : null;

						if (configValue == null)
							continue;

						ConfigFile file;
						FileID id = new FileID(modDll, configValue.file == null ? "main.config" : configValue.file + ".config");

						if (!files.ContainsKey(id))
						{
							file = new ConfigFile(modDll, configValue.file == null ? "main.config" : configValue.file + ".config");
							files.Add(id, file);
						}
						else
						{
							file = files[id];
						}
						IConfigConverter convert = converter?.converter;
						if (uiDesign != null)
							file.AddDesign(configValue.name, uiDesign, category?.category);

						string value = file.Get(configValue.name, convert != null ? convert.ConvertFromValue(configValue.defaultValue) : configValue.defaultValue.ToString(), null, category?.category, description?.description);
						
						try
						{
							field.SetValue(null, Convert.ChangeType(value, field.FieldType));
						}
						catch (Exception e)
						{
							SRML.Console.LogError($"Trying to set value for field '{field.Name}' from config, but config value can't be converted. Default value will be used, however this might mean a field is asking for a diferent type of data then it can contain");
							UnityEngine.Debug.LogException(e);
							field.SetValue(null, configValue.defaultValue);
							continue;
						}
					}
				}
			}
		}

		internal static void SaveFiles()
		{
			foreach (ConfigFile file in files.Values)
			{
				file.Save();
			}
			files.Clear();
		}

		private struct FileID
		{
			public Assembly modDLL;
			public string key;

			public FileID(Assembly modDLL, string key)
			{
				this.modDLL = modDLL;
				this.key = key;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is FileID))
					return false;

				FileID other = (FileID)obj;

				if (other.modDLL == modDLL && other.key.Equals(key))
					return true;

				return false;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
		}
	}
}
