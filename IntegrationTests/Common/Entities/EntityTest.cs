using System;
using System.Data.Entity;
using HelloHome.Common.Entities;

namespace IntegrationTests.Common.Entities
{
    public abstract class EntityTest : IDisposable
    {

        protected HelloHomeDbContext DbCtx;
        private readonly DbContextTransaction _transact;

        protected EntityTest()
        {
           DbCtx = new HelloHomeDbContext();
           _transact = DbCtx.Database.BeginTransaction();
        }

        public void DetachAll()
        {
            foreach (var e in DbCtx.ChangeTracker.Entries ())
                e.State = EntityState.Detached;
        }

        public void Dispose()
        {
            _transact.Rollback();
        }
    }
}