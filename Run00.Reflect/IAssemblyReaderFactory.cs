
namespace Run00.Reflect
{
	public interface IAssemblyReaderFactory
	{
		IAssemblyReader Create(string assemblyPath);
	}
}
