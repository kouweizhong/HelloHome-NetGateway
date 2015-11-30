using System;
using System.Data.Entity;
using log4net;

namespace HelloHome.Common.Entities
{
	public class HelloHomeDbContext : DbContext
	{
		static readonly ILog log = LogManager.GetLogger(typeof(HelloHomeDbContext).Name);

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

		public DbSet<Node> Nodes { get; set; }
		public DbSet<SubNode> SubNodes { get; set; }
		public DbSet<PulseData> PulseData { get; set; }
	}
}

