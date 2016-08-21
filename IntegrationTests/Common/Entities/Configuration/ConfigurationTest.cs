using HelloHome.Common.Entities;
using System.Linq;
using Xunit;

namespace IntegrationTests.Common.Entities.Configuration
{
	public abstract class ConfigurationTest<T> where T : class
	{
		HelloHomeDbContext ctx;

		public ConfigurationTest ()
		{
			ctx = new HelloHomeDbContext ();
		}

		[Fact]
		public void CanCreateEntity ()
		{
			var newE = CreateEntity ();
			ctx.Set<T> ().Add (newE);
			ctx.SaveChanges ();
			foreach (var e in ctx.ChangeTracker.Entries ())
				e.State = System.Data.Entity.EntityState.Detached;
			var eFromDb = ctx.Set<T> ().Single (GetPredicateToRetrieve (newE));
			AssertEquals (newE, eFromDb);
		}

		protected abstract T CreateEntity ();
		protected abstract System.Func<T, bool> GetPredicateToRetrieve (T e);
		protected abstract void AssertEquals (T expected, T actual);
	}
}

