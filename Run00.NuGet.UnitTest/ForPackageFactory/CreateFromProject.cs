using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Run00.AutoMoq;
using Run00.FileSystem;
using Run00.MsTest;
using Run00.Reflect;
using Run00.VStudio;
using System.IO;
using System.Linq;

namespace Run00.NuGet.UnitTest.ForPackageFactory
{
	[DeploymentItem(@"..\..\Artifacts")]
	[TestClass, CategorizeByConventionClass(typeof(CreateFromProject))]
	public class CreateFromProject
	{
		[TestMethod, CategorizeByConvention]
		public void When_Should()
		{
			//Arrange
			var projectFile = "Project.csproj";
			var nuspecFile = "Project.nuspec";
			var nuGetFile = "Project.nupkg";
			var mocker = new Mocker<PackageFactory>();

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.OpenRead(It.Is<string>(s => s == projectFile)))
				.Returns(File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), @"SampleProjectFile.xml")));

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.ChangeFileExtension(It.Is<string>(s => s == projectFile), It.Is<string>(s => s == "nupkg")))
				.Returns(nuGetFile);

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == projectFile)))
				.Returns(string.Empty);

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.CombinePaths(It.IsAny<string>(),  It.Is<string>(s => s == "packages.config")))
				.Returns("packages.config");

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.CombinePaths(It.Is<string>(s => s == @"bin\Release"), It.Is<string>(s => s == "Project.dll")))
				.Returns(@"bin\Release\Project.dll");

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.FindFiles(It.Is<string>(s => s == @"bin\Release"), It.Is<string>(s => s == "*.dll"), It.IsAny<bool>()))
				.Returns(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll").Select(s => Path.GetFileName(s)));

			mocker.GetMock<IFileSystem>()
				.Setup(f => f.OpenWrite(It.Is<string>(s => s == nuGetFile)))
				.Returns(new FileStream(nuGetFile, FileMode.Create, FileAccess.ReadWrite));

			var projectReaderMoq = new Mock<IVsProjectReader>(MockBehavior.Strict);
			projectReaderMoq
				.Setup(r => r.GetBinPath(It.Is<string>(s => s == "Release")))
				.Returns(@"bin\Release");
			projectReaderMoq
				.Setup(r => r.GetAssemblyName())
				.Returns("Project.dll");

			mocker.GetMock<IVsProjectReaderFactory>()
				.Setup(r => r.Create(It.Is<string>(s => s == projectFile)))
				.Returns(projectReaderMoq.Object);

			var assemblyReaderMoq = new Mock<IAssemblyReader>(MockBehavior.Strict);
			assemblyReaderMoq.Setup(r => r.GetCompany()).Returns("Run00");
			assemblyReaderMoq.Setup(r => r.GetCopyright()).Returns("2013");
			assemblyReaderMoq.Setup(r => r.GetDescription()).Returns("Blah Blah");
			assemblyReaderMoq.Setup(r => r.GetFileVersion()).Returns("1.0.0.0");
			assemblyReaderMoq.Setup(r => r.GetPackageId()).Returns("Run00.Product.Title");
			assemblyReaderMoq.Setup(r => r.GetPackageTitle()).Returns("Title for Run00.Product");
			assemblyReaderMoq.Setup(r => r.GetProduct()).Returns("Product");
			assemblyReaderMoq.Setup(r => r.GetTitle()).Returns("Title");

			mocker.GetMock<IAssemblyReaderFactory>()
				.Setup(f => f.Create(It.Is<string>(s => s == @"bin\Release\Project.dll")))
				.Returns(assemblyReaderMoq.Object);

			var packageFactory = mocker.Create() as IPackageFactory;

			//Act
			var result = packageFactory.CreateFromProject(nuspecFile, projectFile, "Release", true);

			//Assert
		}
	}
}
