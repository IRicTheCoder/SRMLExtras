using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRML.Components
{
	public interface IModdedComponent
	{
		void LoadFromOriginal(object original);
	}
}
