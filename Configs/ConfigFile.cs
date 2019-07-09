using System;
using System.IO;
using System.Collections.Generic;

namespace SRMLExtras.Configs
{
	/// <summary>
	/// A config file for reading mod configuration
	/// </summary>
	internal class ConfigFile
	{
		internal readonly Dictionary<string, string> configs = new Dictionary<string, string>();
		internal readonly Dictionary<string, string> descriptions = new Dictionary<string, string>();

		protected string file;

		public ConfigFile(string file)
		{
			string configFolder = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Path)), "Configs");
			this.file = Path.Combine(configFolder, file);
		}

		public short GetShort(string name, short defaultValue, string category = null, string description = null)
		{
			try
			{
				return (short)GetLong(name, defaultValue, category, description);
			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogException(e);
				SRML.Console.LogError($"Value of '{name}' {(category != null ? $"from '{category}'" : "")} can't be converted. Default value will be returned");
				return defaultValue;
			}
		}

		public int GetInt(string name, int defaultValue, string category = null, string description = null)
		{
			try
			{
				return (int)GetLong(name, defaultValue, category, description);
			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogException(e);
				SRML.Console.LogError($"Value of '{name}' {(category != null ? $"from '{category}'" : "")} can't be converted. Default value will be returned");
				return defaultValue;
			}
		}

		public long GetLong(string name, long defaultValue, string category = null, string description = null)
		{
			string key = $"{(category != null ? category.Replace(" ", "") + "." : "")}{name.Replace(" ", "")}";

			if (configs.ContainsKey(key))
			{
				try
				{
					return Convert.ToInt64(configs[key]);
				}
				catch (Exception e)
				{
					UnityEngine.Debug.LogException(e);
					SRML.Console.LogError($"Value of '{name}' {(category != null ? $"from '{category}'" : "")} can't be converted. Default value will be returned");
					return defaultValue;
				}
			}
			else
			{
				SetLong(name, defaultValue, category, description);
				return defaultValue;
			}
		}

		public T Get<T>(string name, T defaultValue, IConfigConverter converter, string category = null, string description = null)
		{
			string key = $"{(category != null ? category.Replace(" ", "") + "." : "")}{name.Replace(" ", "")}";

			if (configs.ContainsKey(key))
			{
				try
				{
					return (T)converter.ConvertToValue(configs[key]);
				}
				catch (Exception e)
				{
					UnityEngine.Debug.LogException(e);
					SRML.Console.LogError($"Value of '{name}' {(category != null ? $"from '{category}'" : "")} can't be converted. Default value will be returned");
					return defaultValue;
				}
			}
			else
			{
				Set(name, defaultValue, converter, category, description);
				return defaultValue;
			}
		}

		public void SetShort(string name, short defaultValue, string category = null, string description = null)
		{
			Set(name, defaultValue, null, category, description);
		}

		public void SetInt(string name, int defaultValue, string category = null, string description = null)
		{
			Set(name, defaultValue, null, category, description);
		}

		public void SetLong(string name, long defaultValue, string category = null, string description = null)
		{
			Set(name, defaultValue, null, category, description);
		}

		public void Set<T>(string name, T value, IConfigConverter converter, string category = null, string description = null)
		{
			string key = $"{(category != null ? category.Replace(" ", "") + "." : "")}{name.Replace(" ", "")}";

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

			if (description != null)
			{
				if (descriptions.ContainsKey(key))
					descriptions.Add(key, description);
				else
					descriptions[key] = description;
			}
		}

		public ConfigFile Load()
		{
			string currKey = "";
			string[] lines = File.ReadAllLines(file);

			configs.Clear();
			foreach (string line in lines)
			{
				if (line.Equals(string.Empty) || line.StartsWith("#"))
					continue;

				if (line.StartsWith("[") && line.EndsWith("]"))
				{
					currKey = line.Substring(1, line.LastIndexOf(']')).Replace(" ", "");
					continue;
				}

				string[] split = line.Split('=');
				configs.Add(currKey + "." + split[0].TrimEnd(' ').Replace(" ", ""), split[1].TrimStart(' '));
			}

			return this;
		}

		public void Save()
		{

		}
	}
}
