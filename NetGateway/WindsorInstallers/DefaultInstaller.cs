using System;
using Castle.MicroKernel.Registration;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.EmonCms;
using Castle.Facilities.TypedFactory;

namespace HelloHome.NetGateway.WindsorInstallers
{
	public class DefaultInstaller : IWindsorInstaller
	{
		private readonly INodeGatewayAgent _nodeAgent;

		public DefaultInstaller ()
		{
			
		}

		public DefaultInstaller (INodeGatewayAgent nodeAgent) 
		{
			_nodeAgent = nodeAgent;
		}

		#region IWindsorInstaller implementation

		public void Install (Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			var kernel = container.Kernel;
			kernel.Resolver.AddSubResolver(new CollectionResolver(kernel));
			container.AddFacility<TypedFactoryFacility> ();


			//Configuration
			container.Register (
				Component.For<IConfigurationProvider, ISerialConfigurationProvider, IEmonCmsConfiguration> ().ImplementedBy<AppSettingsBasedConfiguration> ()
			);

			//dbContext
			container.Register(Component.For<HelloHomeDbContext>().LifestyleTransient().Named("PipelineFreeDbContext"));

			//Agents
			if(_nodeAgent == null)
				container.Register (Component.For<INodeGatewayAgent> ()
				                    .ImplementedBy<NodeGatewayAgent>()
				                    .LifestyleSingleton());
			else
				container.Register (Component.For<INodeGatewayAgent> ()
				                    .Instance(_nodeAgent));
			
			container.Register (Component.For<IEmonCmsAgent> ().ImplementedBy<EmonCmsAgent>());

			//Parsers & encoders
			container.Register(Classes.FromAssemblyContaining<IMessageParser>().BasedOn<IMessageParser>().WithServiceBase());
			container.Register(
				Classes.FromAssemblyContaining<IMessageEncoder>().BasedOn<IMessageEncoder>().WithServiceBase(),
				Component.For<PinConfigEncoder>()
			);

			//EmonCmsUpdater
			container.Register(Component.For<IEMonCmsUpdater>().ImplementedBy<EMonCmsUpdater>());

			//HelloHomeGateway
			container.Register(Component.For<HelloHomeGateway>());
		}

		#endregion
	}
}

