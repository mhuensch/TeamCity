using Run00.ComponentRegistration;
using System;

namespace Run00.Setup.Windsor
{
	public class Registration<T> : IComponent<T>
		where T : class
	{
		public Type Service { get { return typeof(T); } }
		public bool HasSetup { get { return SingletonSetup != null; } }

		public Type Implementation { get; private set; }
		public string Name { get; private set; }
		public bool RegisteredAsSingleton { get; private set; }
		public Action<T> SingletonSetup { get; private set; }

		public IComponent<T> ImplementedBy<TImpl>() where TImpl : T
		{
			Implementation = typeof(TImpl);
			return this;
		}

		public IComponent<T> Named(string name)
		{
			Name = name;
			return this;
		}

		public IComponent<T> AsSingleton()
		{
			return AsSingleton(null);
		}

		public IComponent<T> AsSingleton(Action<T> action)
		{
			RegisteredAsSingleton = true;
			SingletonSetup = action;
			return this;
		}

		public void RunSetup(object obj)
		{
			if (SingletonSetup == null)
				throw new InvalidOperationException("RunSetup() not configured for this component");

			if (RegisteredAsSingleton == false)
				throw new InvalidOperationException("RunSetup() can not be called on a component that is not a singleton");

			SingletonSetup.Invoke((T)obj);
			SingletonSetup = null;
		}
	}
}