using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Run00.ComponentRegistration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CastleWindsor = Castle.MicroKernel.Registration;

namespace Run00.Setup.Windsor
{
	public static class WindsorRegistration
	{
		public static CastleWindsor.IRegistration[] RegisterFrom(string baseDir)
		{
			var result = new List<CastleWindsor.IRegistration>();
			var installers = LocateAll<IComponentRegistration>(baseDir);
			foreach (var installer in installers)
			{
				var components = new RegistrationCollection();
				installer.Register(components);
				foreach (var registration in components.Components)
				{
					var r = new ComponentRegistration<object>(registration.Service);

					if (registration.Implementation != null)
						r = r.ImplementedBy(registration.Implementation);

					if (string.IsNullOrWhiteSpace(registration.Name) == false)
						r = r.Named(registration.Name);

					if (registration.RegisteredAsSingleton)
					{
						r = r.LifeStyle.Singleton;
						if (registration.HasSetup)
							r.OnCreate((f, p) => registration.RunSetup(p));
					}

					result.Add(r);
				}
			}
			return result.ToArray();
		}

		private static IEnumerable<T> LocateAll<T>(string baseDir)
		{
			var result = new List<T>();
			using (var container = new WindsorContainer())
			{
				var dirs = Directory.GetDirectories(baseDir, "*.*", SearchOption.AllDirectories).Union(new[] { baseDir });
				foreach (var dir in dirs)
					container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(dir)).BasedOn<T>().WithServiceFromInterface(typeof(T)).AllowMultipleMatches());

				try
				{
					result = container.ResolveAll<T>().ToList();
					return result;
				}
				finally
				{
					foreach (var r in result)
						container.Release(r);
				}
			}
		}
	}
}