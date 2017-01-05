using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using HelloHome.Common.Entities;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Handlers;
using HelloHome.NetGateway.WindsorInstallers;
using Moq;

namespace End2EndTests
{
    public class TestableGateway : IDisposable
    {
        public HelloHomeDbContext DbCtx;
        readonly WindsorContainer _ioCcontainer;

        public TestableGateway()
        {
            //Database.SetInitializer (new MySqlInitializer ());
            Database.SetInitializer<HelloHomeDbContext>(null);

            _ioCcontainer = new WindsorContainer();
            _ioCcontainer.Install(new DefaultInstaller());
            //Overrides some registration
            _ioCcontainer.Register(
                Component.For<IEMonCmsUpdater>().Instance(new Mock<IEMonCmsUpdater>().Object).IsDefault()
            );

            DbCtx = _ioCcontainer.Resolve<HelloHomeDbContext>("SingletonDbContext");
            DbCtx.Database.Delete();
            DbCtx.Database.Create();
        }

        public NodeGateway CreateGateway(INodeMessageChannel channel)
        {
            var handlerFactory = _ioCcontainer.Resolve<IMessageHandlerFactory>();
            return  new NodeGateway(channel, handlerFactory);
        }

        private byte _nextRfId = 100;

        public byte GetNextRfId()
        {
            lock (typeof(TestableGateway))
                return _nextRfId++;
        }

        public void ReleaseDbContext(HelloHomeDbContext ctx)
        {
            _ioCcontainer.Release(ctx);
        }

        public Mock<T> Mock<T>() where T : class
        {
            var mock = new Mock<T>();
            _ioCcontainer.Register(
                Component.For<T>()
                    .Instance(mock.Object)
                    .Named(Guid.NewGuid().ToString())
                    .IsDefault()
            );
            return mock;
        }

        public void Dispose()
        {
            _ioCcontainer.Dispose();
        }
    }
}