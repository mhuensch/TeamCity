using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NDesk.Options;
using Run00.Application;
using Run00.NuGet;
using Run00.Setup.Windsor;
using System;
using System.IO;

namespace Run00.TeamCityChocolatey
{
	class Program
	{
		static void Main(string[] args)
		{
			var nuGetUrl = string.Empty;
			var nuGetKey = string.Empty;
			var solutionFile = string.Empty;

			var options = new OptionSet() {
				{ "f|SolutionFile=", "The solution {FILE} that contains the projects and nuspec files to build", f => solutionFile = f },
				{ "n|NuGetUrl=", "The Chocolatey server {URL} to push to.", n => nuGetUrl = n },
				{ "p|NuGetKey=", "The account {KEY} for the Chocolatey server to push to.", k => nuGetKey = k },
			};
			var extra = options.Parse(args);

			if (extra.Count > 0 || string.IsNullOrWhiteSpace(nuGetUrl) || string.IsNullOrWhiteSpace(nuGetKey) || string.IsNullOrWhiteSpace(solutionFile))
			{
				options.WriteOptionDescriptions(Console.Out);
				return;
			}

			var container = new WindsorContainer();
			var publisher = default(IPublisher);

			var registrations = WindsorRegistration.RegisterFrom(Directory.GetCurrentDirectory());
			container.Register(registrations);
			container.Register(Component.For<IApplicationInfoAccessor>().ImplementedBy<LocalApplicationInfoAccessor>());
			container.Register(Component.For<INugetServerSettings>().UsingFactoryMethod(() => new NuGetServerSettings(nuGetUrl, nuGetKey)));

			try
			{
				publisher = container.Resolve<IPublisher>();
				publisher.PublishPackages(solutionFile, "Chocolatey.nuspec", "Release", true);
			}
			finally
			{
				if (publisher != null)
					container.Release(publisher);
			}
		}
	}
}
