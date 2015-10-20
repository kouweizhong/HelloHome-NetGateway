using System;
using System.Data.Entity;
using System.Reflection;

namespace HelloHome.Common.Entities
{
	public class HelloHomeDbContext : DbContext
	{
		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly (Assembly.GetExecutingAssembly ());
			Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Node> Nodes { get; set; }
		public DbSet<SubNode> SubNodes { get; set; }
		public DbSet<PulseData> PulseData { get; set; }
	}
}

