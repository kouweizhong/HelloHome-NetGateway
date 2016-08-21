using HelloHome.Common.Entities;
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
			var node = CreateEntity ();
			ctx.Set<T> ().Add (node);
			ctx.SaveChanges ();
		}

		protected abstract T CreateEntity ();
	}
}

