using Run00.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Run00.VStudio
{
	public class VsProjectReader : IVsProjectReader
	{
		public VsProjectReader(XDocument xml)
		{
			_xml = xml;
		}

		string IVsProjectReader.GetBinPath(string configuration)
		{
			var releases = _xml
				.Descendants(GetMsBuildXName("Project"))
				.Descendants(GetMsBuildXName("PropertyGroup"))
				.Where(p => p.Attribute("Condition") != null && p.Attribute("Condition").Value.Contains(configuration));

			//var path = default(XElement);
			//if (release != null)
			//	path = release.Descendants(GetMsBuildXName("OutputPath")).FirstOrDefault();

			return string.Empty;
		}

		private XName GetMsBuildXName(string localName)
		{
			return XName.Get(localName, "http://schemas.microsoft.com/developer/msbuild/2003");
		}

		string IVsProjectReader.GetAssemblyName()
		{
			throw new NotImplementedException();
		}

		private readonly XDocument _xml;
	}
}
