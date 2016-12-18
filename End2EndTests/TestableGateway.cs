using System;
using System.Data.Entity;
using System.Transactions;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using HelloHome.Common.Entities;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.WindsorInstallers;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace End2EndTests
{
	public class TestableGateway : IDisposable
	{
		public Mock<INodeGatewayAgent> NodeAgent;
		public HelloHomeGateway Gateway;

		public readonly HelloHomeDbContext DbContext;

		readonly WindsorContainer _IoCcontainer;

		public TestableGateway ()
		{
			//Database.SetInitializer (new MySqlInitializer ());
			Database.SetInitializer (new DropCreateDatabaseAlways<HelloHomeDbContext>());
			NodeAgent = new Mock<INodeGatewayAgent> ();

			_IoCcontainer = new WindsorContainer ();
			_IoCcontainer.Install (new DefaultInstaller (
				Component.For (typeof (IEMonCmsUpdater)).Instance (new Mock<IEMonCmsUpdater> ().Object),
				Component.For (typeof (INodeGatewayAgent)).Instance (NodeAgent.Object)
			));

			Gateway = _IoCcontainer.Resolve<HelloHomeGateway> ();
			DbContext = _IoCcontainer.Resolve<HelloHomeDbContext> ("TransientDbContext");
		}

		public Mock<T> Mock<T> () where T : class
		{			
			var mock = new Mock<T> ();
			_IoCcontainer.Register (
				Component.For<T>()
				.Instance (mock.Object)
				.Named(Guid.NewGuid ().ToString ())
				.IsDefault ()
			);
			return mock;
		}

		public void Dispose ()
		{
			DbContext.Database.Delete ();
			DbContext.Database.Create ();
			_IoCcontainer.Release (Gateway);
			_IoCcontainer.Release (DbContext);
			_IoCcontainer.Dispose ();
		}
	}
}
