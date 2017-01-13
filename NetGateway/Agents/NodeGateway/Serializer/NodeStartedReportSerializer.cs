using System;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    [SerializerFor(typeof(NodeStartedReport), MessageIdentifier)]
    public class NodeStartedReportSerializer : IncomingMessageSerializer<NodeStartedReport>
    {
        public const byte MessageIdentifier = 0 + 1 << 2;

        protected override NodeStartedReport DeserializeInternal(byte[] bytes)
        {
            if (bytes.Length > 13)
                return new NodeStartedReport {
                    FromNodeId = bytes [0],
                    Rssi = BitConverter.ToInt16 (bytes, 1),
                    Major = bytes [4],
                    Minor = bytes [5],
                    OldSignature = BitConverter.ToInt32 (bytes, 6),
                    Signature = BitConverter.ToInt64 (bytes, 10),
                    NeedNewRfAddress = BitConverter.ToBoolean (bytes, 18),
                };
            return new NodeStartedReport {
                FromNodeId = bytes [0],
                Rssi = (int)BitConverter.ToInt16 (bytes, 1),
                Major = bytes [4],
                Minor = bytes [5],
                OldSignature = BitConverter.ToInt32 (bytes, 6),
                NeedNewRfAddress = BitConverter.ToBoolean (bytes, 10),
            };
        }

        protected override IEnumerable<byte> SerializeInternal(NodeStartedReport msg)
        {
            yield return msg.FromNodeId;
            var rssi = BitConverter.GetBytes((short) msg.Rssi);
            yield return rssi[0];
            yield return rssi[1];
        }
    }
}