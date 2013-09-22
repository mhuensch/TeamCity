using Run00.ComponentRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Setup.Windsor
{
	public class RegistrationCollection : IComponentCollection
	{
		internal IEnumerable<IComponent> Components { get { return _components; } }

		IComponent<T> IComponentCollection.AddFor<T>()
		{
			var result = new Registration<T>();
			_components.Add(result);
			return result;
		}

		private readonly List<IComponent> _components = new List<IComponent>();
	}
}
