using Castle.MicroKernel.Registration;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.EmonCms;
using Castle.Facilities.TypedFactory;
using HelloHome.NetGateway.Handlers;
using HelloHome.NetGateway.Queries;
using HelloHome.NetGateway.Commands;
using System;
using System.Linq;
using HelloHome.Common;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;

namespace HelloHome.NetGateway.WindsorInstallers
{
    public class DefaultInstaller : IWindsorInstaller
    {
        private readonly IRegistration[] _overrides;

        #region IWindsorInstaller implementation

        public DefaultInstaller(params IRegistration[] overrides)
        {
            _overrides = overrides;
        }

        public void Install(Castle.Windsor.IWindsorContainer container,
            Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            var kernel = container.Kernel;
            kernel.Resolver.AddSubResolver(new CollectionResolver(kernel));
            container.AddFacility<TypedFactoryFacility>();


            //Configuration
            container.Register(
                Component.For<IConfigurationProvider, ISerialConfigurationProvider, IEmonCmsConfiguration>()
                    .ImplementedBy<AppSettingsBasedConfiguration>()
            );

            //dbContext
            container.Register(
                _overrides.LastOrDefault(_ => _ is ComponentRegistration<IHelloHomeDbContext>) ??
                Component.For<IHelloHomeDbContext>().ImplementedBy<HelloHomeDbContext>().LifestyleBoundTo<IMessageHandler>());

            container.Register(Component.For<HelloHomeDbContext>().LifestyleTransient().Named("TransientDbContext"));
            container.Register(Component.For<HelloHomeDbContext>().LifestyleSingleton().Named("SingletonDbContext"));

            //Agents
            container.Register(Component.For<INodeGatewayAgent>()
                .ImplementedBy<NodeGatewayAgent>()
                .LifestyleSingleton());

            container.Register(Component.For<IEmonCmsAgent>().ImplementedBy<EmonCmsAgent>());

            //Parsers & encoders
            container.Register(Classes.FromAssemblyContaining<IMessageParser>()
                .BasedOn<IMessageParser>()
                .WithServiceBase());
            container.Register(
                Classes.FromAssemblyContaining<IMessageEncoder>().BasedOn<IMessageEncoder>().WithServiceBase(),
                Component.For<PinConfigEncoder>()
            );

            //EmonCmsUpdater
            container.Register(
                _overrides.LastOrDefault(_ => _ is ComponentRegistration<IEMonCmsUpdater>)??
                Component.For<IEMonCmsUpdater>().ImplementedBy<EMonCmsUpdater>());

            //HelloHomeGateway
            container.Register(Component.For<NodeGateway>());
            container.Register(Component.For<INodeMessageChannel>().ImplementedBy<NodeMessageSerialChannel>());
            container.Register(Component.For<IByteStream>().ImplementedBy<SerialPortByteStream>());

            //MessageHandlers
            container.Register(
                Classes.FromAssemblyContaining<IMessageHandler>()
                    .BasedOn(typeof(MessageHandler<>))
                    .WithServiceSelf()
                    .LifestyleTransient(),
                Component.For<MessageHandlerComponentSelector>()
                    .LifestyleSingleton(),
                Component.For<IMessageHandlerFactory>()
                    .AsFactory(typeof(MessageHandlerComponentSelector))
            );

            //Queries & Commands
            container.Register(Classes.FromAssemblyContaining<IQuery>()
                .BasedOn<IQuery>()
                .WithServiceAllInterfaces()
                .LifestyleBoundTo<IMessageHandler>());
            container.Register(Classes.FromAssemblyContaining<ICommand>()
                .BasedOn<ICommand>()
                .WithServiceAllInterfaces()
                .LifestyleBoundTo<IMessageHandler>());

            //Logic
            container.Register(
                Component.For<ITimeProvider>().ImplementedBy<TimeProvider>(),
                Component.For<IRfIdGenerationStrategy>()
                    .ImplementedBy<FindHoleRfIdGenerationStrategy>()
                    .LifestyleBoundTo<IMessageHandler>()
            );
            TimeProvider.Current = container.Resolve<ITimeProvider>();
        }

        #endregion
    }
}