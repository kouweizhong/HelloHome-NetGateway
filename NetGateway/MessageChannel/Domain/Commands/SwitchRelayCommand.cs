using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Commands
{
    public class SwitchRelayCommand : OutgoingMessage
    {
        public byte SwitchActuatorNumber { get; set; }
        public bool NewState { get; set; }
    }
}