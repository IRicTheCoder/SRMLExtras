using System;
using System.IO;
using System.Reflection;

namespace SRMLExtras.Configs
{
	internal class ConfigHandler
	{
		/// <summary>
		/// Copies all config files from the DLL into the mod's folder. Ignores files that are already present
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>The text inside the file</returns>
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
	}
}
