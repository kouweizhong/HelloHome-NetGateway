using System;
using System.Data.Entity;
using NLog;

namespace HelloHome.Common.Entities
{
    public interface IHelloHomeDbContext
    {
        IDbSet<Node> Nodes { get; }
    }

    public class HelloHomeDbContext : DbContext, IHelloHomeDbContext
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public HelloHomeDbContext ()
		{
			//Database.Log = Console.Write;
		}

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly (typeof(HelloHomeDbContext).Assembly);
			Configuration.LazyLoadingEnabled = false;	
		}

		public Guid ContextId { get; } = Guid.NewGuid();

		public IDbSet<Node> Nodes { get; set; }
		public IDbSet<NodePort> SubNodes { get; set; }
		public IDbSet<PulseData> PulseData { get; set; }
	}
}

