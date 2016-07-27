using System;
using Castle.MicroKernel.Lifestyle.Scoped;
using System.Collections.Concurrent;
using HelloHome.NetGateway.Pipeline;
using Castle.MicroKernel;

namespace HelloHome.NetGateway.WindsorInstallers
{
	public class PerProcessingContextScopeAccessor : IScopeAccessor
	{
		private readonly ConcurrentDictionary<Guid, ILifetimeScope> collection = new ConcurrentDictionary<Guid, ILifetimeScope>();
		private readonly DefaultLifetimeScope Default = new DefaultLifetimeScope ();

		#region IScopeAccessor implementation
		public ILifetimeScope GetScope (Castle.MicroKernel.Context.CreationContext context)
		{
			if (ProcessingContext.Current == null)
				return Default;
			return collection.GetOrAdd(ProcessingContext.Current.ContextId, id => new DefaultLifetimeScope());
		}
		#endregion

		#region IDisposable implementation
		public void Dispose ()
		{
			foreach (var scope in collection)
			{
				scope.Value.Dispose();
			}
			collection.Clear();
		}
		#endregion

	}
}

