using System;
using System.Data.Entity;
using System.Reflection;

namespace NetHhGateway.Entities
{
	public class HelloHomeDbContext : DbContext
	{
		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly (Assembly.GetExecutingAssembly ());
		}

		public DbSet<Node> Nodes { get; set; }
		public DbSet<SubNode> SubNodes { get; set; }
		public DbSet<PulseData> PulseData { get; set; }
	}
}

