using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Reflect
{
	public interface IAssemblyReader
	{
		string GetCompany();
		string GetProduct();
		string GetTitle();
		string GetDescription();
		string GetCopyright();
		string GetFileVersion();
		TAttribute GetCustomAttribute<TAttribute>();

		string GetPackageId();

		string GetPackageTitle();
	}
}
