using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Tags a variable to receive a config value
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class ConfigValueAttribute : Attribute
	{
		public string file;
		public string name;
		public object defaultValue;

		/// <summary>
		/// Tags a variable to receive a config value
		/// </summary>
		/// <param name="name">The name of the value</param>
		/// <param name="defaultValue">The default value in case the config isn't found</param>
		public ConfigValueAttribute(string name, object defaultValue)
		{
			this.name = name;
			this.defaultValue = defaultValue;
			file = null;
		}

		/// <summary>
		/// Tags a variable to receive a config value
		/// </summary>
		/// <param name="name">The name of the value</param>
		/// <param name="file">The file where the value is stored</param>
		/// <param name="defaultValue">The default value in case the config isn't found</param>
		public ConfigValueAttribute(string name, string file, object defaultValue)
		{
			this.name = name;
			this.defaultValue = defaultValue;
			this.file = file.Replace("\\", "/");
		}
	}
}
