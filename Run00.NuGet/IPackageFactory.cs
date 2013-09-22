using NuGet;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Run00.NuGet
{
	[ContractClass(typeof(ContractForIPackageFactory))]
	public interface IPackageFactory
	{
		IPackage CreateFromProject(string nupecFile, string csprojFile, string buildConfiguration, bool includeBinOutput);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(IFileSystem))]
	internal abstract class ContractForIPackageFactory : IPackageFactory
	{
		IPackage IPackageFactory.CreateFromProject(string nupecFile, string csprojFile, string buildConfiguration, bool includeBinOutput)
		{
			Contract.Requires(string.IsNullOrWhiteSpace(csprojFile) == false);
			throw new NotImplementedException();
		}
	}
}
