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

			foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Chocolatey.nuspec"))
			{
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
