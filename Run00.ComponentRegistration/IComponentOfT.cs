using System;

namespace Run00.ComponentRegistration
{
	public interface IComponent<T> : IComponent where T : class
	{
		IComponent<T> ImplementedBy<TImpl>() where TImpl : T;

		IComponent<T> Named(string name);

		IComponent<T> AsSingleton();

		IComponent<T> AsSingleton(Action<T> action);

		Action<T> SingletonSetup { get; }
	}
}