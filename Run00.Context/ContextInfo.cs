using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Run00.Context
{
	/// <summary>
	/// The context of the user.
	/// </summary>
	/// <remarks>In most use cases, this POCO is retrieved from <see cref="IContextInfoAccessor.Current"/>.</remarks>
	[Serializable]
	public class ContextInfo
	{
		public Guid UserId { get; private set; }
		public Guid ApplicationId { get; private set; }

		/// <summary>
		/// Initializes a new instance of the Context class.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="email">The email.</param>
		/// <param name="tenantId">The tenant id.</param>
		/// <param name="serverId">The server id.</param>
		/// <param name="hostId">The host id.</param>
		/// <param name="applicationId">The application id.</param>
		/// <param name="applicationName">The application name.</param>
		/// <param name="tenantName">The tenant name.</param>
		/// <param name="siteName">The site name.</param>
		/// <param name="userName">The user name.</param>
		/// <param name="lastLogin">The last login time.</param>
		public ContextInfo(Guid userId, Guid applicationId)
		{
			Contract.Requires(userId != Guid.Empty);
			Contract.Requires(applicationId != Guid.Empty);

			this.UserId = userId;
			this.ApplicationId = applicationId;
		}

		[ContractInvariantMethod]
		[ExcludeFromCodeCoverage]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Required for code contracts.")]
		private void ObjectInvariant()
		{
			Contract.Invariant(UserId != Guid.Empty);
			Contract.Invariant(ApplicationId != Guid.Empty);
		}
	}
}
