using System;
using System.IO;
using System.Reflection;

namespace SRML.Configs
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
			if (!Directory.Exists(configFolder))
				Directory.CreateDirectory(configFolder);

			// SEARCHS ALL FILES IN THE MANIFEST AND CREATES THEM
			// FILES NEED TO BE STORED IN FOLDER "Resources/Configs"
			foreach (string fileWithNamespace in modDll.GetManifestResourceNames())
			{
				UnityEngine.Debug.Log("File: " + fileWithNamespace);

				string file = fileWithNamespace.Substring(fileWithNamespace.IndexOf('.') + 1);
				if (!file.StartsWith("Resources.Configs.") || !file.EndsWith(".config"))
					continue;

				string pathFile = file.Replace("Resources.Configs.", "").Replace(".config", "").Replace('.', '/') + ".config";

				UnityEngine.Debug.Log("FileNew: " + pathFile);

				if (File.Exists(Path.Combine(configFolder, pathFile)))
					continue;

				if (!Directory.Exists(Path.Combine(configFolder, Path.GetDirectoryName(pathFile))))
					Directory.CreateDirectory(Path.Combine(configFolder, Path.GetDirectoryName(pathFile)));

				UnityEngine.Debug.Log("DirectoryCreate: " + pathFile);

				using (Stream stream = Main.execAssembly.GetManifestResourceStream(fileWithNamespace))
				{
					UnityEngine.Debug.Log("FileStream: " + stream);

					using (StreamReader reader = new StreamReader(stream))
					{
						File.WriteAllText(Path.Combine(configFolder, pathFile), reader.ReadToEnd());
					}
				}
			}
		}
	}
}
