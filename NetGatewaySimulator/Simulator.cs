using NetGatewaySimulator.Logger;

namespace NetGatewaySimulator
{
    public class Simulator
    {
        private readonly ILogger _logger;

        public Simulator(ILogger logger)
        {
            _logger = logger;
        }
    }
}