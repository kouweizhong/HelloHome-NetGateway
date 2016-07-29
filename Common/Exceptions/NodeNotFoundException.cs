using System;

namespace Common.Exceptions
{
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException(int rfId)
            :base($"Node with rfId {rfId} not found.")
        {
            
        }
    }
}