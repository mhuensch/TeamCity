using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.NuGet
{
	public class NuGetServer : INuGetServer
	{
		public NuGetServer(INugetServerSettings settings)
		{
			_settings = settings;
		}

		void INuGetServer.PushPackage(IPackage package)
		{
			var server = new PackageServer(_settings.Url, "NuGet Command Line");
			server.PushPackage(_settings.Key, package, 10000);
		}

		private readonly INugetServerSettings _settings;
	}
}
