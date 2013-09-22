using log4net.Core;
using Run00.Application;
using Run00.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Run00.LoggingLog4Net
{
	/// <summary>
	/// An implementation of <see cref="ILog"/> that uses log4net. This class uses the log4net config file for its configurations.
	/// </summary>
	public sealed class Log : ILog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
		/// </summary>
		/// <param name="applicationAccessor">The container.</param>
		public Log(IApplicationInfoAccessor applicationAccessor)
		{
			Contract.Requires(applicationAccessor != null);
			_applicationAccessor = applicationAccessor;
		}

		void ILog.Debug(string message)
		{
			FindLogger().Debug(message);
		}

		void ILog.Info(string message)
		{
			FindLogger().Info(message);
		}

		void ILog.Warning(string message)
		{
			FindLogger().Warn(message);
		}

		void ILog.Error(string message, Exception exception)
		{
			FindLogger().Error(message, exception);
		}

		void ILog.Fatal(string message, Exception exception)
		{
			FindLogger().Fatal(message, exception);
		}

		private log4net.ILog FindLogger()
		{
			Contract.Ensures(Contract.Result<ILog>() != null);

			var applicationName = string.Empty;
			if (_applicationAccessor.Current != null)
				applicationName = _applicationAccessor.Current.Name;

			var log = log4net.LogManager.GetLogger(applicationName);
			if (log == null)
				throw new LogException(string.Format(CultureInfo.InvariantCulture, "No logger was available for the application and no default logger was provided."));

			return log;
		}

		private readonly IApplicationInfoAccessor _applicationAccessor;

		[ContractInvariantMethod]
		[ExcludeFromCodeCoverage]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Required for code contracts.")]
		private void ObjectInvariant()
		{
			Contract.Invariant(_applicationAccessor != null);
		}
	}
}
