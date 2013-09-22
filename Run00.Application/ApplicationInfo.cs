using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Application
{
	public class ApplicationInfo
	{
		public string Name { get; private set; }

		public ApplicationInfo(string name)
		{
			Contract.Requires(string.IsNullOrWhiteSpace(name) == false);
			Name = name;
		}
	}
}
