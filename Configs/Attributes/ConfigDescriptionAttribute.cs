using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Tags a variable with a description to add to the config
	/// Requires a ConfigValueAttribue to function
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ConfigDescriptionAttribute : Attribute
	{
		public string description;

		/// <summary>
		/// Tags a variable with a description to add to the config
		/// Requires a ConfigValueAttribue to function
		/// </summary>
		/// <param name="description">The description to add, \n can be used to create multi lines</param>
		public ConfigDescriptionAttribute(string description)
		{
			this.description = description;
		}
	}
}
