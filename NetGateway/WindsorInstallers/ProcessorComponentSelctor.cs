using System;
using Castle.Facilities.TypedFactory;
using HelloHome.NetGateway.Processors;

namespace HelloHome.NetGateway.WindsorInstallers
{
	public class ProcessorComponentSelector : DefaultTypedFactoryComponentSelector
	{
		protected override Type GetComponentType (System.Reflection.MethodInfo method, object[] arguments)
		{
			var messageType = arguments [0].GetType ();
			var genType = typeof(MessageProcessor<>);
			var  procType = genType.MakeGenericType (messageType);
			return procType;
		}
	}
}

