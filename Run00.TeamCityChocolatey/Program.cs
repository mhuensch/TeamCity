using System;
using System.IO;
using System.Linq;

namespace Run00.TeamCityChocolatey
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting chocolatey packaging process");

			var sourceDir = args[0];
			Console.WriteLine("Using directory: " + sourceDir);

			foreach (var file in Directory.GetFiles(sourceDir, "Chocolatey.nuspec", SearchOption.AllDirectories))
			{
				Console.WriteLine("Running against chocolatey file: " + file);

				var projectFile = Directory.GetFiles(Path.GetDirectoryName(file), "*.csproj").SingleOrDefault();
				if (projectFile == null)
				{
					Console.WriteLine("Could not find .csproj file for: " + file);
					throw new InvalidOperationException();
				}

				Console.WriteLine("Running packaging for: " + file + " using " + projectFile);
			}
		}
	}
}
