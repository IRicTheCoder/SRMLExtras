using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace SRMLExtras.Configs
{
	/// <summary>
	/// A config file for reading mod configuration
	/// </summary>
	public class ConfigFile
	{
		// CONSTANTS TO PREVENT GARBAGE WHEN READING FILES
		private const string CAT_SEPARATOR = "-------------------------";
		private const string SPACE = " ";
		private const string COMMENT = "# ";
		private const string CAT_OPEN = "[";
		private const string CAT_CLOSE = "]";
		private const string NEWLINE = "\n";

		internal readonly Dictionary<string, string> configs = new Dictionary<string, string>();
		internal readonly Dictionary<string, ConfigInfo> infos = new Dictionary<string, ConfigInfo>();
		internal readonly Dictionary<string, Attributes.ConfigUIAttribute> uiDesigns = new Dictionary<string, Attributes.ConfigUIAttribute>();

		protected string file;

		public ConfigFile(string file)
		{
			string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)), "Configs");
			this.file = Path.Combine(configFolder, file);
		}

		public ConfigFile(Assembly modDll, string file)
		{
			string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(modDll.CodeBase).Path)), "Configs");
			this.file = Path.Combine(configFolder, file);
		}

		internal void AddDesign(string name, Attributes.ConfigUIAttribute uiDesign, string category = null)
		{
			string key = $"{(category != null ? category.Replace(SPACE, string.Empty) + "." : string.Empty)}{name.Replace(SPACE, string.Empty)}";

			if (!uiDesigns.ContainsKey(key))
				uiDesigns.Add(key, uiDesign);
			else
				uiDesigns[key] = uiDesign;
		}

		public T Get<T>(string name, T defaultValue, IConfigConverter converter, string category = null, string description = null)
		{
			string key = $"{(category != null ? category.Replace(SPACE, string.Empty) + "." : string.Empty)}{name.Replace(SPACE, string.Empty)}";

			if (configs.ContainsKey(key))
			{
				try
				{
					return converter != null ? (T)converter.ConvertToValue(configs[key]) : (T)Convert.ChangeType(configs[key], typeof(T));
				}
				catch (Exception e)
				{
					UnityEngine.Debug.LogException(e);
					SRML.Console.LogError($"Value of '{name}' {(category != null ? $"from '{category}'" : string.Empty)} can't be converted. Default value will be returned");
					return defaultValue;
				}
			}
			else
			{
				Set(name, defaultValue, converter, category, description);
				return defaultValue;
			}
		}

		public void Set<T>(string name, T value, IConfigConverter converter, string category = null, string description = null)
		{
			string key = $"{(category != null ? category.Replace(SPACE, string.Empty) + "." : string.Empty)}{name.Replace(SPACE, string.Empty)}";

			if (converter == null)
			{
				if (configs.ContainsKey(key))
					configs[key] = value.ToString();
				else
					configs.Add(key, value.ToString());
			}
			else
			{
				if (configs.ContainsKey(key))
					configs[key] = converter.ConvertFromValue(value);
				else
					configs.Add(key, converter.ConvertFromValue(value));
			}

			if (infos.ContainsKey(key))
				infos.Add(key, new ConfigInfo(name, description, category));
			else
				infos[key] = new ConfigInfo(name, description, category);
		}

		public ConfigFile Load()
		{
			string currKey = string.Empty;
			string nextDescription = string.Empty;
			string[] lines = File.ReadAllLines(file);

			configs.Clear();
			infos.Clear();
			foreach (string line in lines)
			{
				if (line.Equals(string.Empty) || line.StartsWith(CAT_SEPARATOR))
					continue;

				if (line.StartsWith(CAT_OPEN) && line.EndsWith(CAT_CLOSE))
				{
					currKey = line.Substring(1, line.LastIndexOf(']')-1) + ".";
					continue;
				}

				if (line.StartsWith(COMMENT))
				{
					nextDescription += $"{line.Substring(2)}\n";
					continue;
				}

				string[] split = line.Split('=');
				string key = $"{currKey.Replace(SPACE, string.Empty)}{split[0].TrimEnd(' ').Replace(SPACE, string.Empty)}";

				configs.Add(key, split[1].TrimStart(' ').Replace(NEWLINE, string.Empty));
				infos.Add(key, new ConfigInfo(split[0].TrimEnd(' '), nextDescription.Equals(string.Empty) ? null : nextDescription.Substring(0, nextDescription.LastIndexOf("\n")), currKey.Equals(string.Empty) ? null : currKey.Substring(0, currKey.Length - 1)));

				nextDescription = string.Empty;
			}

			return this;
		}

		public void Save()
		{
			List<string> lines = new List<string>();
			List<string> keys = new List<string>(configs.Keys);
			keys.Sort((ka, kb) =>
			{
				if (!ka.Contains(".") && kb.Contains("."))
					return -1;

				if (ka.Contains(".") && !kb.Contains("."))
					return 1;

				return ka.CompareTo(kb);
			});

			string lastCategory = string.Empty;
			foreach (string key in keys)
			{
				if (infos[key].categoryName != null && !lastCategory.Equals(infos[key].categoryName))
				{
					lines.Add(CAT_SEPARATOR);
					lines.Add(string.Empty); // adds new line
					lines.Add($"[{infos[key].categoryName}]");
					lines.Add(string.Empty); // adds new line
					lastCategory = infos[key].categoryName;
				}

				if (infos[key].description != null)
				{
					foreach (string descLine in infos[key].description.Split('\n'))
						lines.Add($"# {descLine}");
				}

				lines.Add($"{infos[key].displayName} = {configs[key]}");

				lines.Add(string.Empty); // adds new line
			}

			lines.RemoveAt(lines.Count - 1);

			if (!Directory.Exists(Path.GetDirectoryName(file)))
				Directory.CreateDirectory(Path.GetDirectoryName(file));

			if (!File.Exists(file))
				File.Create(file).Close();

			File.WriteAllLines(file, lines.ToArray());
		}
		
		internal struct ConfigInfo
		{
			public string categoryName;
			public string displayName;
			public string description;

			public ConfigInfo(string displayName, string description, string categoryName)
			{
				this.description = description;
				this.displayName = displayName;
				this.categoryName = categoryName;
			}
		}
	}
}
