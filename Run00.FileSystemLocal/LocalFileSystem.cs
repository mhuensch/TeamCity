using Run00.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.FileSystemLocal
{
	public class LocalFileSystem : IFileSystem
	{
		IEnumerable<string> IFileSystem.FindFiles(string directory, string searchPattern, bool includeSubDirectories)
		{
			if (includeSubDirectories)
				return Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);

			return Directory.GetFiles(directory, searchPattern, SearchOption.TopDirectoryOnly);
		}

		string IFileSystem.ChangeFileExtension(string fileName, string newExtension)
		{
			return Path.ChangeExtension(fileName, newExtension);
		}

		string IFileSystem.GetDirectory(string fileName)
		{
			return Path.GetDirectoryName(fileName);
		}

		string IFileSystem.CombinePaths(string root, string path)
		{
			return Path.Combine(root, path);
		}

		Stream IFileSystem.OpenRead(string fileName)
		{
			return File.OpenRead(fileName);
		}

		Stream IFileSystem.OpenWrite(string fileName)
		{
			return File.OpenWrite(fileName);
		}
	}
}
