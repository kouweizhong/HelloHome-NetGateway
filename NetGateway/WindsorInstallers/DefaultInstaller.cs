using System;
using Castle.MicroKernel.Registration;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using HelloHome.NetGateway.Processors;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.EmonCms;
using Castle.Facilities.TypedFactory;
using HelloHome.NetGateway.Pipeline;

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
			container.Register(Component.For<HelloHomeDbContext>().LifestyleScoped<PerProcessingContextScopeAccessor>());
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

			//Pipeline
			container.Register (
				Component.For<IGatewayPipelineFactory>().AsFactory(),
				Component.For<IGatewayPipeline>().ImplementedBy<GatewayPipeline>().LifestyleTransient(),
				Component.For<IPipelineModuleFactory>().AsFactory(),
				Classes.FromAssemblyContaining<IPipelineModuleFactory>().BasedOn<IPipelineModule>().WithServiceBase().WithServiceSelf().LifestyleScoped<PerProcessingContextScopeAccessor>()
			);

			//Processors	
			container.Register(Component.For<ProcessorComponentSelector>());
			container.Register(Component.For<IMessageProcessorFactory>().AsFactory(typeof(ProcessorComponentSelector)));
			container.Register(Classes.FromAssemblyContaining<IMessageProcessor>().BasedOn(typeof(MessageProcessor<>)).WithServiceBase().Configure(_=>_.LifestyleScoped<PerProcessingContextScopeAccessor>()));
			container.Register (Component.For<IRfNodeIdGenerationStrategy> ().ImplementedBy<FindHoleRfNodeIdGenerationStrategy> ());

			//EmonCmsUpdater
			container.Register(Component.For<IEMonCmsUpdater>().ImplementedBy<EMonCmsUpdater>());

			//HelloHomeGateway
			container.Register(Component.For<HelloHomeGateway>());
		}

		#endregion
	}
}

