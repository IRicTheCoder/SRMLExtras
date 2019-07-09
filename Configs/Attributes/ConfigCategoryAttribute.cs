using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Defines a category inside the config file when using ConfigValueAttribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ConfigCategoryAttribute : Attribute
	{
		public string category;

		/// <summary>
		/// Defines a category inside the config file when using ConfigValueAttribute
		/// </summary>
		/// <param name="category">The name of the category</param>
		public ConfigCategoryAttribute(string category)
		{
			this.category = category;
		}
	}
}
