using Run00.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.TeamCityChocolatey
{
	public class LocalApplicationInfoAccessor : IApplicationInfoAccessor
	{
		ApplicationInfo IApplicationInfoAccessor.Current { get { return _applicationInfo; } }

		public LocalApplicationInfoAccessor()
		{
			_applicationInfo = new ApplicationInfo("Team City Chocolatey");
		}

		private readonly ApplicationInfo _applicationInfo;
	}
}
