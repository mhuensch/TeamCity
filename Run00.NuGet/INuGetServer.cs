using NuGet;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Run00.NuGet
{
	[ContractClass(typeof(ContractForINuGetServer))]
	public interface INuGetServer
	{
		void PushPackage(IPackage package);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(INuGetServer))]
	internal abstract class ContractForINuGetServer : INuGetServer
	{
		void INuGetServer.PushPackage(IPackage package)
		{
			Contract.Requires(package != null);
			throw new System.NotImplementedException();
		}
	}
}
