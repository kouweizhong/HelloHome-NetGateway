using System;
using System.Data.Entity;
using System.Transactions;
using HelloHome.Common.Entities;

namespace IntegrationTests.Common.Entities
{
    public class EntityTestFixture : IDisposable
    {

        public HelloHomeDbContext DbCtx;
        private readonly DbContextTransaction _transact;

        public EntityTestFixture()
        {
           DbCtx = new HelloHomeDbContext();
           _transact = DbCtx.Database.BeginTransaction();
        }

        public void DetachAll()
        {
            foreach (var e in DbCtx.ChangeTracker.Entries ())
                e.State = System.Data.Entity.EntityState.Detached;
        }

        public void Dispose()
        {
            _transact.Rollback();
        }
    }
}