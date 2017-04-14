using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Domain.Commands
{
    public class SwitchRelayCommand : OutgoingMessage
    {
        public byte SwitchActuatorNumber { get; set; }
        public bool NewState { get; set; }
    }
}