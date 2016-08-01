using System;
using System.Linq;
using Castle.Facilities.TypedFactory;
using HelloHome.NetGateway.Handlers;

namespace HelloHome.NetGateway.WindsorInstallers
{
	public class MessageHandlerComponentSelector : DefaultTypedFactoryComponentSelector
	{
		ILookup<Type, Type> typeLookup = null;

		public MessageHandlerComponentSelector ()
		{
			var types = typeof (MessageHandler<>)
				.Assembly
				.GetTypes ()
				.Where (x => IsSubclassOfRawGeneric(typeof(MessageHandler<>), x) && x != typeof(MessageHandler<>))
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

		static bool IsSubclassOfRawGeneric (Type generic, Type toCheck)
		{
			while (toCheck != null && toCheck != typeof (object)) {
				var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition () : toCheck;
				if (generic == cur) {
					return true;
				}
				toCheck = toCheck.BaseType;
			}
			return false;
		}
	}
}

