using System;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Threading.Tasks;
using NLog;

namespace HelloHome.Common.Entities
{
    public interface IHelloHomeDbContext
    {
        IDbSet<Node> Nodes { get; }
		int Commit ();
        Task<int> CommitAsync();
    }

    public class HelloHomeDbContext : DbContext, IHelloHomeDbContext
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public HelloHomeDbContext ()
        {
            Database.Log = Logger.Debug;
        }

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly (typeof(HelloHomeDbContext).Assembly);
			Configuration.LazyLoadingEnabled = false;	
		}

		public int Commit ()
		{
			try {
				return SaveChanges ();
			} catch (Exception e) {
				throw e;
			}
		}

        public async Task<int> CommitAsync()
        {
            try {
                return await SaveChangesAsync();
            } catch (Exception e) {
                throw e;
            }
        }

        public Guid ContextId { get; } = Guid.NewGuid();

		public IDbSet<Node> Nodes { get; set; }
		public IDbSet<Port> Ports { get; set; }
        public DbSet<PortGroup> PortGroups { get; set; }
        public IDbSet<PulseHistory> PulseData { get; set; }
        public IDbSet<Trigger> Triggers { get; set; }
    }
}

