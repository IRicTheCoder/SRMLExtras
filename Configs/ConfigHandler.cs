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

		// COPIES THE DEFAULT FILES IF THEY EXIST
		internal static void CopyFiles(Assembly modDll)
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

		// READS ALL CONFIG FILES FROM A GIVEN ASSEMBLY, THIS CACHES VALUES TO USE DURING POPULATE, THIS PREVENTS MOST OVERHEAD
		// MAKING SETTING VALUES THROUGH ATTRIBUTES SAFE, CAUSE NO OTHER INSTRUCTION WILL RUN DURING THIS PROCESS
		private static void ReadFiles()
		{
			foreach (Assembly modDll in AppDomain.CurrentDomain.GetAssemblies())
			{
				string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(modDll.CodeBase).Path)), "Configs");
				foreach (string file in Directory.GetFiles(configFolder, "*", SearchOption.AllDirectories))
				{
					string fileRelative = file.Replace(configFolder + "\\", "");
					files.Add(new FileID(modDll, fileRelative), new ConfigFile(file).Load());
				}
			}
		}


		// POPULATES EVERY CLASS WITH THE CONFIG VALUES. THESE VALUES CAN STILL BE OBTAINED IN A CONVENTIONAL WAY THROUGH FILES
		internal static void Populate()
		{
			foreach (Assembly modDll in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type type in modDll.GetTypes())
				{
					foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
					{
						ConfigValueAttribute configValue = field.GetCustomAttributes(typeof(ConfigValueAttribute), false)?[0] as ConfigValueAttribute;
						ConfigDescriptionAttribute description = field.GetCustomAttributes(typeof(ConfigDescriptionAttribute), false)?[0] as ConfigDescriptionAttribute;
						ConfigCategoryAttribute category = field.GetCustomAttributes(typeof(ConfigCategoryAttribute), false)?[0] as ConfigCategoryAttribute;
						ConfigConverterAttribute converter = field.GetCustomAttributes(typeof(ConfigConverterAttribute), false)?[0] as ConfigConverterAttribute;

						ConfigFile file = files[new FileID(modDll, configValue.file ?? "main.config")];
						IConfigConverter convert = converter.converter;
						
					}
				}
			}
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
