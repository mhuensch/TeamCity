using Run00.NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.TeamCityChocolatey
{
	public class NuGetServerSettings : INugetServerSettings
	{
		string INugetServerSettings.Url { get { return _url; } }
		string INugetServerSettings.Key { get { return _key; } }

		public NuGetServerSettings(string url, string key)
		{
			_url = url;
			_key = key;
		}

		private readonly string _url;
		private readonly string _key;
	}
}
