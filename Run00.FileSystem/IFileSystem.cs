using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;

namespace Run00.FileSystem
{
	[ContractClass(typeof(ContractForIFileSystem))]
	public interface IFileSystem
	{
		IEnumerable<string> FindFiles(string directory, string searchPattern, bool includeSubDirectories);
		string ChangeFileExtension(string fileName, string newExtension);
		string GetDirectory(string fileName);
		string CombinePaths(string root, string path);
		Stream OpenRead(string fileName);
		Stream OpenWrite(string fileName);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(IFileSystem))]
	internal abstract class ContractForIFileSystem : IFileSystem
	{
		IEnumerable<string> IFileSystem.FindFiles(string directory, string searchPattern, bool includeSubDirectories)
		{
			Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
			throw new System.NotImplementedException();
		}

		string IFileSystem.ChangeFileExtension(string fileName, string newExtension)
		{
			Contract.Ensures(string.IsNullOrWhiteSpace(Contract.Result<string>()) == false);
			throw new System.NotImplementedException();
		}

		string IFileSystem.GetDirectory(string fileName)
		{
			Contract.Ensures(string.IsNullOrWhiteSpace(Contract.Result<string>()) == false);
			throw new System.NotImplementedException();
		}

		string IFileSystem.CombinePaths(string root, string path)
		{
			throw new System.NotImplementedException();
		}

		Stream IFileSystem.OpenRead(string fileName)
		{
			Contract.Ensures(Contract.Result<Stream>() != null);
			throw new System.NotImplementedException();
		}

		Stream IFileSystem.OpenWrite(string fileName)
		{
			Contract.Ensures(Contract.Result<Stream>() != null);
			throw new System.NotImplementedException();
		}
	}
}
