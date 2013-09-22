using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Context
{
	/// <summary>
	/// Holds the context in the host's ambient user session.
	/// </summary>
	public interface IContextInfoAccessor
	{
		ContextInfo Current { get; }
	}
}
