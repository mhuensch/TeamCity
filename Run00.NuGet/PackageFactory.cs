using NuGet;
using Run00.Reflect;
using Run00.VStudio;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Run00.NuGet
{
	public class PackageFactory : IPackageFactory
	{
		public PackageFactory(Run00.FileSystem.IFileSystem fileSystem, IVsProjectReaderFactory projectReaderFactory, IAssemblyReaderFactory assemblyReaderFactory)
		{
			_fileSystem = fileSystem;
			_projectReaderFactory = projectReaderFactory;
			_assemblyReaderFactory = assemblyReaderFactory;
		}

		IPackage IPackageFactory.CreateFromProject(string nupecFile, string csprojFile, string buildConfiguration, bool includeBinOutput)
		{
			var projectReader = _projectReaderFactory.Create(csprojFile);
			var binDir = projectReader.GetBinPath(buildConfiguration);
			var assemblyName = projectReader.GetAssemblyName();
			var assemblyPath = _fileSystem.CombinePaths(binDir, assemblyName);
			var assemblyReader = _assemblyReaderFactory.Create(assemblyPath);

			var manifest = new ManifestMetadata()
			{
				Id = assemblyReader.GetPackageId(),
				Title = assemblyReader.GetPackageTitle(),
				Owners = assemblyReader.GetCompany(),
				Authors = assemblyReader.GetCompany(),
				Description = assemblyReader.GetDescription(),
				Copyright = assemblyReader.GetCopyright(),
				Version = assemblyReader.GetFileVersion()
			};

			var files = new List<ManifestFile>();
			foreach (var dll in _fileSystem.FindFiles(binDir, "*.dll", false))
				files.Add(new ManifestFile() { Source = dll, Target = @"lib\net40" });

			var packageBuilder = new PackageBuilder();
			packageBuilder.Populate(manifest);
			packageBuilder.PopulateFiles(string.Empty, files);

			var projDir = _fileSystem.GetDirectory(csprojFile);
			var packagefile = _fileSystem.CombinePaths(projDir, "packages.config");

			var packagePath = _fileSystem.ChangeFileExtension(csprojFile, "nupkg");
			using (var stream = _fileSystem.OpenWrite(packagePath))
			{
				packageBuilder.Save(stream);
			}

			return new ZipPackage(packagePath);
		}

		private readonly Run00.FileSystem.IFileSystem _fileSystem;
		private readonly IVsProjectReaderFactory _projectReaderFactory;
		private readonly IAssemblyReaderFactory _assemblyReaderFactory;
	}
}
