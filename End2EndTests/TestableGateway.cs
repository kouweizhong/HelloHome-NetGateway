using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using HelloHome.Common.Entities;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.WindsorInstallers;
using Moq;

namespace End2EndTests
{
	public class TestableGateway : IDisposable
	{
	    public Mock<INodeMessageChannel> MessageReader;
		public NodeGateway Gateway;

		public readonly HelloHomeDbContext DbContext;

		readonly WindsorContainer _ioCcontainer;

		public TestableGateway ()
		{
			//Database.SetInitializer (new MySqlInitializer ());
			Database.SetInitializer (new DropCreateDatabaseAlways<HelloHomeDbContext>());
			MessageReader = new Mock<INodeMessageChannel> ();

			_ioCcontainer = new WindsorContainer ();
		    _ioCcontainer.Install(new DefaultInstaller());
		    //Overrides some registration
		    _ioCcontainer.Register(
				Component.For<IEMonCmsUpdater>().Instance (new Mock<IEMonCmsUpdater> ().Object).IsDefault(),
				Component.For<INodeMessageChannel>().Instance (MessageReader.Object).IsDefault()
			);

			Gateway = _ioCcontainer.Resolve<NodeGateway> ();
			DbContext = _ioCcontainer.Resolve<HelloHomeDbContext> ("TransientDbContext");
		}

		public Mock<T> Mock<T> () where T : class
		{			
			var mock = new Mock<T> ();
			_ioCcontainer.Register (
				Component.For<T>()
				.Instance (mock.Object)
				.Named(Guid.NewGuid ().ToString ())
				.IsDefault ()
			);
			return mock;
		}

		public void Dispose ()
		{
			_ioCcontainer.Release (Gateway);
			_ioCcontainer.Release (DbContext);
			_ioCcontainer.Dispose ();
		}
	}
}
