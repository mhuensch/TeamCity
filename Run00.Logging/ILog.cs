using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.Logging
{
	/// <summary>
	/// Logging Interface as a System Service.
	/// All logging operations to persistent or non-persistent systems go through this interface.
	/// </summary>
	[ContractClass(typeof(LogContractClass))]
	public interface ILog
	{
		/// <summary>
		/// Used to log detailed messages that can be used by developers to debug possible issues.
		/// Use this level to write detailed messages that capture user actions with input values and results and environment/configuration(s).
		/// </summary>
		/// <param name="message">the string to log</param>
		void Debug(string message);

		/// <summary>
		/// Used to log informatory messages that capture user actions and application flow.
		/// Use this level to write messages that capture user actions in details along with input values.
		/// Any special behavior that is triggered by user action should be tracked using this.
		/// </summary>
		/// <param name="message">the string to log</param>
		void Info(string message);

		/// <summary>
		/// Used to log messages that capture possible non-generic situations and application behavior in such cases.
		/// Use this level to write messages that capture potentially harmful or illegal action(s).
		/// </summary>
		/// <param name="message">the string to log</param>
		void Warning(string message);

		/// <summary>
		/// Used to log messages that capture error situations and application behavior in such cases.
		/// Use this level to log errors that won't cause the application to fail.
		/// </summary>
		/// <param name="message">the string to log</param>
		/// <param name="exception">the exception to log</param>
		void Error(string message, Exception exception);

		/// <summary>
		/// Used to log messages that capture severe error conditions that may cause the application to fail.
		/// Use this level to log errors that the system may not be able to handle in a safe way.
		/// </summary>
		/// <param name="message">the string to log</param>
		/// <param name="exception">the exception to log</param>
		void Fatal(string message, Exception exception);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(ILog))]
	internal abstract class LogContractClass : ILog
	{
		void ILog.Debug(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		void ILog.Info(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		void ILog.Warning(string message)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		void ILog.Error(string message, Exception exception)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			throw new NotImplementedException();
		}

		void ILog.Fatal(string message, Exception exception)
		{
			Contract.Requires(string.IsNullOrEmpty(message) == false);
			Contract.Requires(exception != null);
			throw new NotImplementedException();
		}
	}
}
