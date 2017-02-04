namespace HelloHome.NetGateway.MessageChannel.Parsers.Factory
{
    public interface IMessageParserFactory
    {
        IMessageParser Create(byte[] bytes);
        void Release(IMessageParser parser);
    }
}