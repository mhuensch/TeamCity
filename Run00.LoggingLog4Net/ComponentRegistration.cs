using Run00.ComponentRegistration;
using Run00.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.LoggingLog4Net
{
	public class ComponentRegistration : IComponentRegistration
	{
		void IComponentRegistration.Register(IComponentCollection registrations)
		{
			registrations.AddFor<ILog>().ImplementedBy<Log>();
		}
	}
}
