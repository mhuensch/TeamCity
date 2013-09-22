namespace Run00.NuGet
{
	public interface IPublisher
	{
		void PublishPackages(string solutionFile, string nuspecFilePattern, string buildConfiguration, bool includeBinOutput);
	}
}
