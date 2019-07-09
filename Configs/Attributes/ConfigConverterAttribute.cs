using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Defines the converter used to get the value from a ConfigValueAttribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ConfigConverterAttribute : Attribute
	{
		public IConfigConverter converter;

		/// <summary>
		/// Defines the converter used to get the value from a ConfigValueAttribute
		/// </summary>
		/// <param name="converter">The converter to use</param>
		public ConfigConverterAttribute(IConfigConverter converter)
		{
			this.converter = converter;
		}
	}
}
