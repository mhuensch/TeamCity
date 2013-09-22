using Run00.ComponentRegistration;
using Run00.FileSystem;

namespace Run00.FileSystemLocal
{
	public class ComponentRegistration : IComponentRegistration
	{
		void IComponentRegistration.Register(IComponentCollection registrations)
		{
			registrations.AddFor<IFileSystem>().ImplementedBy<LocalFileSystem>();
		}
	}
}
