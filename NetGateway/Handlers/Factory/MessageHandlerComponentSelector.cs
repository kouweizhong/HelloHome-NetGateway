using System;
using System.Linq;
using Castle.Facilities.TypedFactory;
using Common.Extentions;

namespace HelloHome.NetGateway.Handlers.Factory
{
	public class MessageHandlerComponentSelector : DefaultTypedFactoryComponentSelector
	{
	    readonly ILookup<Type, Type> typeLookup = null;

		public MessageHandlerComponentSelector ()
		{
			var types = typeof (MessageHandler<>)
				.Assembly
				.GetTypes ()
				.Where (x => x.IsSubclassOfRawGeneric(typeof(MessageHandler<>)) && x != typeof(MessageHandler<>))
				.ToList ();
			typeLookup = types.ToLookup (x => x.BaseType.GetGenericArguments ().Single (), x => x);
		}

		protected override Type GetComponentType (System.Reflection.MethodInfo method, object [] arguments)
		{
			var type = typeLookup[arguments.Single().GetType()].FirstOrDefault();
			if (type == null)
				throw new Exception ($"No message handler found for {arguments[0].GetType().Name}");
			return type;
		}
	}
}

