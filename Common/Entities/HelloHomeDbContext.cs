using System;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Diagnostics;
using System.Threading.Tasks;
using NLog;

namespace HelloHome.Common.Entities
{
    public interface IHelloHomeDbContext
    {
        Guid ContextId { get; }

        IDbSet<Node> Nodes { get; }
        IDbSet<Port> Ports { get; set; }
        IDbSet<PortGroup> PortGroups { get; set; }
        IDbSet<PulseHistory> PulseData { get; set; }
        IDbSet<Trigger> Triggers { get; set; }

        int Commit ();
        Task<int> CommitAsync();
    }

    public class HelloHomeDbContext : DbContext, IHelloHomeDbContext
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public HelloHomeDbContext ()
        {
            //Database.Log = Logger.Debug;
        }

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly (typeof(HelloHomeDbContext).Assembly);
			Configuration.LazyLoadingEnabled = false;	
		}

		public int Commit ()
		{
			try {
			    Logger.Debug("Commiting in context {0}", ContextId);
			    return SaveChanges ();
			} catch (Exception e) {
				throw e;
			}
		}

        public async Task<int> CommitAsync()
        {
            try {
                Logger.Debug("Commiting in context {0}", ContextId);
                return await SaveChangesAsync();
            } catch (Exception e) {
                throw e;
            }
        }

        public Guid ContextId { get; } = Guid.NewGuid();

		public IDbSet<Node> Nodes { get; set; }
		public IDbSet<Port> Ports { get; set; }
        public IDbSet<PortGroup> PortGroups { get; set; }
        public IDbSet<PulseHistory> PulseData { get; set; }
        public IDbSet<Trigger> Triggers { get; set; }
    }
}

