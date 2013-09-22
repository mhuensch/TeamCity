using Run00.ComponentRegistration;

namespace Run00.NuGet
{
	public class ComponentRegistration : IComponentRegistration
	{
		void IComponentRegistration.Register(IComponentCollection registrations)
		{
			registrations.AddFor<IPublisher>().ImplementedBy<Publisher>();
			registrations.AddFor<INuGetServer>().ImplementedBy<NuGetServer>();
			registrations.AddFor<IPackageFactory>().ImplementedBy<PackageFactory>();
		}
	}
}
