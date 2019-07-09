using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Tags a variable to receive a config value
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ConfigValueAttribute : Attribute
	{
		public Type valueType;
		public string file;
		public string name;
		public object defaultValue;

		/// <summary>
		/// Tags a variable to receive a config value
		/// </summary>
		/// <param name="valueType">The type of value to read</param>
		/// <param name="name">The name of the value </param>
		/// <param name="defaultValue">The default value in case the config isn't found</param>
		public ConfigValueAttribute(Type valueType, string name, object defaultValue)
		{
			this.valueType = valueType;
			this.name = name;
			this.defaultValue = defaultValue;
			file = null;
		}

		/// <summary>
		/// Tags a variable to receive a config value
		/// </summary>
		/// <param name="valueType">The type of value to read</param>
		/// <param name="name">The name of the value </param>
		/// <param name="name">The name of the value </param>
		/// <param name="defaultValue">The default value in case the config isn't found</param>
		public ConfigValueAttribute(Type valueType, string name, string file, object defaultValue)
		{
			this.valueType = valueType;
			this.name = name;
			this.defaultValue = defaultValue;
			this.file = file;
		}
	}
}
