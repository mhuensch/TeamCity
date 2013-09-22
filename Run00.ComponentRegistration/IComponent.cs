using System;

namespace Run00.ComponentRegistration
{
	public interface IComponent
	{
		Type Service { get; }
		Type Implementation { get; }
		string Name { get; }
		bool RegisteredAsSingleton { get; }
		bool HasSetup { get; }
		void RunSetup(object obj);
	}
}