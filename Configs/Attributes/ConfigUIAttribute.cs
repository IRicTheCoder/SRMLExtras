using System;

namespace SRMLExtras.Configs.Attributes
{
	/// <summary>
	/// Base UI method
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public abstract class ConfigUIAttribute : Attribute
	{
		/// <summary>
		/// Draws the value in the UI
		/// </summary>
		/// <param name="value">Takes the current UI</param>
		/// <returns>The resulting value if changed or the current one if no chage was made</returns>
		public abstract object DrawValue(object value);
	}
}
