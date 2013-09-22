using Run00.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Run00.VStudio
{
	public class VsProjectReaderFactory : IVsProjectReaderFactory
	{
		public VsProjectReaderFactory(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		IVsProjectReader IVsProjectReaderFactory.Create(string csprojPath)
		{
			var xDoc = default(XDocument);
			using (var projStream = _fileSystem.OpenRead(csprojPath))
			{
				xDoc = XDocument.Load(projStream);
			}
			return new VsProjectReader(xDoc);
		}

		private readonly IFileSystem _fileSystem;
	}
}
