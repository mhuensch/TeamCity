namespace Run00.VStudio
{
	public interface IVsProjectReader
	{
		string GetBinPath(string configuration);
		string GetAssemblyName();
	}
}
