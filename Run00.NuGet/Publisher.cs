using Run00.FileSystem;
using Run00.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.NuGet
{
	public class Publisher : IPublisher
	{
		public Publisher(ILog log, IFileSystem fileSystem, IPackageFactory packageFactory, INuGetServer packageServer)
		{
			_log = log;
			_fileSystem = fileSystem;
			_packageFactory = packageFactory;
			_packageServer = packageServer;
		}

		void IPublisher.PublishPackages(string solutionFile, string nuspecFilePattern, string buildConfiguration, bool includeBinOutput)
		{
			_log.Info("Starting NuGet packaging process for solution: " + solutionFile);

			var solutionDir = _fileSystem.GetDirectory(solutionFile);
			foreach (var nuspecFile in _fileSystem.FindFiles(solutionDir, nuspecFilePattern, true))
			{
				_log.Info("_Creating NuGet Package_");
				_log.Info("Using nuspec file: " + nuspecFile);

				var projectDir = _fileSystem.GetDirectory(nuspecFile);
				var projectFile = _fileSystem.FindFiles(projectDir, "*.csproj", false).SingleOrDefault();
				if (string.IsNullOrWhiteSpace(projectFile) == true)
					throw new InvalidOperationException("Could not find .csproj file related to: " + nuspecFile);

				var package = _packageFactory.CreateFromProject(nuspecFile, projectFile, buildConfiguration, includeBinOutput);
				_packageServer.PushPackage(package);
			}
		}

		private readonly ILog _log;
		private readonly INuGetServer _packageServer;
		private readonly IPackageFactory _packageFactory;
		private readonly IFileSystem _fileSystem;
	}
}
