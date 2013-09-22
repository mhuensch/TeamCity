using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NuGet;
using Run00.AutoMoq;
using Run00.Logging;
using Run00.MsTest;
using System;

namespace Run00.NuGet.UnitTest.ForPublisher
{
	[TestClass, CategorizeByConventionClass(typeof(PublishPackages))]
	public class PublishPackages
	{
		[TestMethod, CategorizeByConvention]
		public void WhenAllSolutionDataExists_ShouldPublishPackagesForEachNuspec()
		{
			//Arrange
			var mocker = new Mocker<Publisher>();

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == _defaultSolutionFile)))
				.Returns(_defaultSolutionDir);

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == _defaultNuspec1Path)))
				.Returns(_defaultProject1Dir);

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == _defaultNuspec2Path)))
				.Returns(_defaultProject2Dir);

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.FindFiles(
					It.Is<string>(s => s == _defaultSolutionDir),
					It.Is<string>(s => s == _defaultNuspecPattern),
					It.Is<bool>(b => b == true))
				).Returns(new string[] { _defaultNuspec1Path, _defaultNuspec2Path });

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.FindFiles(
					It.Is<string>(s => s == _defaultProject1Dir),
					It.Is<string>(s => s == "*.csproj"),
					It.Is<bool>(b => b == false))
				).Returns(new string[] { _defaultProject1Path });

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.FindFiles(
					It.Is<string>(s => s == _defaultProject2Dir),
					It.Is<string>(s => s == "*.csproj"),
					It.Is<bool>(b => b == false))
				).Returns(new string[] { _defaultProject2Path });

			var project1Package = new Mock<IPackage>();
			mocker.GetMock<IPackageFactory>()
				.Setup(f => f.CreateFromProject(
					It.Is<string>(s => s == _defaultNuspec1Path),
					It.Is<string>(s => s == _defaultProject1Path),
					It.IsAny<string>(),
					It.IsAny<bool>())
				).Returns(project1Package.Object);

			var project2Package = new Mock<IPackage>();
			mocker.GetMock<IPackageFactory>()
				.Setup(f => f.CreateFromProject(
					It.Is<string>(s => s == _defaultNuspec2Path),
					It.Is<string>(s => s == _defaultProject2Path),
					It.IsAny<string>(),
					It.IsAny<bool>())
				).Returns(project2Package.Object);

			mocker.GetMock<INuGetServer>()
				.Setup(s => s.PushPackage(It.Is<IPackage>(p => p == project1Package.Object || p == project2Package.Object)));

			mocker.SetBehavior<ILog>(MockBehavior.Loose);

			var publisher = mocker.Create() as IPublisher;

			//Act
			publisher.PublishPackages(_defaultSolutionFile, _defaultNuspecPattern, "Release", false);

			//Assert
			mocker.VerifyAll();
		}

		[TestMethod, CategorizeByConvention]
		public void WhenNuspecDoesNotHaveMatchingProject_ShouldThrowInvalidOperation()
		{
			//Arrange
			var mocker = new Mocker<Publisher>();

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == _defaultSolutionFile)))
				.Returns(_defaultSolutionDir);

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.GetDirectory(It.Is<string>(s => s == _defaultProject1Path)))
				.Returns(_defaultProject1Dir);

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.FindFiles(
					It.Is<string>(s => s == _defaultSolutionDir),
					It.Is<string>(s => s == _defaultNuspecPattern),
					It.Is<bool>(b => b == true))
				).Returns(new string[] { _defaultProject1Path });

			mocker.GetMock<Run00.FileSystem.IFileSystem>()
				.Setup(f => f.FindFiles(
					It.Is<string>(s => s == _defaultProject1Dir),
					It.Is<string>(s => s == "*.csproj"),
					It.Is<bool>(b => b == false))
				).Returns(new string[] { });

			mocker.SetBehavior<ILog>(MockBehavior.Loose);

			var publisher = mocker.Create() as IPublisher;

			//Act
			var exception = ExceptionTest.Catch(() => publisher.PublishPackages(_defaultSolutionFile, _defaultNuspecPattern, "Release", false));

			//Assert
			mocker.VerifyAll();
			Assert.AreEqual(typeof(InvalidOperationException), exception.GetType());
		}

		private readonly string _defaultNuspecPattern = "*.mynuspec";
		private readonly string _defaultSolutionDir = @"C:\SourceCode\Solution";
		private readonly string _defaultSolutionFile = @"C:\SourceCode\Solution\MySolution.sln";
		private readonly string _defaultNuspec1Path = @"C:\SourceCode\Solution\Project1\Project1.csproj";
		private readonly string _defaultProject1Path = @"C:\SourceCode\Solution\Project1\Project1.mynuspec";
		private readonly string _defaultProject1Dir = @"C:\SourceCode\Solution\Project1";
		private readonly string _defaultProject2Path = @"C:\SourceCode\Solution\Project1\Project2.csproj";
		private readonly string _defaultNuspec2Path = @"C:\SourceCode\Solution\Project1\Project2.mynuspec";
		private readonly string _defaultProject2Dir = @"C:\SourceCode\Solution\Project2";
	}
}
