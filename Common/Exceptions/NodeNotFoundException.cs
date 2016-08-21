using System;

namespace HelloHome.Common.Exceptions
{
    [Serializable]
    public class NodeNotFoundException : HelloHomeException
    {
        public NodeNotFoundException(int rfId)
            : base("NODE_NOT_FOUND", $"Node with rfId {rfId} not found.")
        {

        }
        public NodeNotFoundException(long signature)
            : base("NODE_NOT_FOUND", $"Node with signature {signature} not found.")
        {

        }
    }
}