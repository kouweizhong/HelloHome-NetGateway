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
using HelloHome.NetGateway.Handlers;
using HelloHome.NetGateway.Queries;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Logic;

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
			container.Register (Component.For<IHelloHomeDbContext> ().ImplementedBy<HelloHomeDbContext>() .LifestyleBoundTo<IMessageHandler> ());

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

			//MessageHandlers
			container.Register (
				Classes.FromAssemblyContaining<IMessageHandler> ()
					.BasedOn (typeof (MessageHandler<>))
					.WithServiceSelf ()
					.LifestyleTransient (),
				Component.For<MessageHandlerComponentSelector> ()
					.LifestyleSingleton(),
				Component.For<IMessageHandlerFactory> ()
					.AsFactory(typeof(MessageHandlerComponentSelector))
			);

			//Queries
			container.Register (Classes.FromAssemblyContaining<IQuery> ().BasedOn<IQuery> ().WithServiceFirstInterface ().LifestyleBoundTo<IMessageHandler> ());
			container.Register (Classes.FromAssemblyContaining<ICommand> ().BasedOn<ICommand> ().WithServiceFirstInterface ().LifestyleBoundTo<IMessageHandler> ());

			//Logic
			container.Register (
				Component.For<ITimeProvider>().ImplementedBy<TimeProvider>().LifestyleBoundTo<IMessageHandler>(),
				Component.For<ITouchNode> ().ImplementedBy<TouchNode> ().LifestyleBoundTo<IMessageHandler> (),
				Component.For<IRfIdGenerationStrategy> ().ImplementedBy<FindHoleRfIdGenerationStrategy> ().LifestyleBoundTo<IMessageHandler> ()
			);
		}

		#endregion
	}
}

