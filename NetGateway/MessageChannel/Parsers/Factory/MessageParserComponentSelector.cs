﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace HelloHome.NetGateway.MessageChannel.Parsers.Factory
{
    public class MessageParserComponentSelector : DefaultTypedFactoryComponentSelector
    {
        private readonly Dictionary<byte, Type> _cache = new Dictionary<byte, Type>();
        public MessageParserComponentSelector()
        {
            _cache = typeof(IMessageParser).Assembly.GetTypes()
                .Where(x => typeof(IMessageParser).IsAssignableFrom(x)
                            && x.GetCustomAttribute<ParserForAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<ParserForAttribute>().DiscrimatorByte);
        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            if (method.ReturnType != typeof(IMessageParser))
                throw new Exception($"{nameof(MessageParserComponentSelector)} is meant ti use with IMessageParserFactory only. Check your container configuration.");

            var bytes = (byte[]) arguments[0];
            if (bytes[0] == '/' && bytes[1] == '/')
                return typeof(CommentParser);

            Type parserType;
            return _cache.TryGetValue(bytes[3], out parserType) ? parserType : typeof(ParseAllParser);
        }
    }
}