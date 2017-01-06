using System.Collections.Generic;
using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreatePortGroupTests : IClassFixture<EntityTestFixture>
    {
        private readonly EntityTestFixture _fixture;
        private HelloHomeDbContext _dbCtx;
        private const long BaseSignature = 76587486538549;
        private const byte TestNetwork = 66;

        public CreatePortGroupTests(EntityTestFixture fixture)
        {
            _fixture = fixture;
            _dbCtx = fixture.DbCtx;
        }

        [Fact]
        public void create_port_group()
        {
            var nodes = new List<Node>
            {
                new Node { RfNetwork = TestNetwork, RfAddress = 1, Signature = BaseSignature + 1, Ports = new List<Port>
                {
                    new PulsePort { Number = 1 },
                    new SwitchPort { Number = 2 }

                }},
                new Node { RfNetwork = TestNetwork, RfAddress = 2, Signature = BaseSignature + 2, Ports = new List<Port>
                {
                    new PulsePort { Number = 1 },
                    new SwitchPort { Number = 2 }
                }},
            };
            foreach (var n in nodes)
                _dbCtx.Nodes.Add(n);
            _dbCtx.SaveChanges();
            var portGroup = new PortGroup
            {
                Name = "TestGroup",
                Ports = new List<Port>
                {
                    nodes[0].Ports[0],
                    nodes[1].Ports[0],
                    nodes[1].Ports[1],
                }
            };
            _dbCtx.PortGroups.Add(portGroup);
            _dbCtx.SaveChanges();
        }
    }
}