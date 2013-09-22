using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Application
{
	public interface IApplicationInfoAccessor
	{
		ApplicationInfo Current { get; }
	}
}
