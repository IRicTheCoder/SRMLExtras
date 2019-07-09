using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRMLExtras.Configs
{
	public interface IConfigConverter
	{
		string ConvertFromValue(object value);
		object ConvertToValue(string value);
	}
}
