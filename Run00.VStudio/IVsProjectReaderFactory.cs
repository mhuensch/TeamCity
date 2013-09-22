
namespace Run00.VStudio
{
	public interface IVsProjectReaderFactory
	{
		IVsProjectReader Create(string csprojPath);
	}
}
