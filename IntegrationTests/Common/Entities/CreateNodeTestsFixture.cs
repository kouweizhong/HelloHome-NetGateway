using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Common.Extentions;
using HelloHome.Common.Entities;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Common.Entities
{
    public class CreateNodeTestsFixture : IClassFixture<EntityTestFixture>
    {
        private readonly EntityTestFixture _fixture;
        private readonly HelloHomeDbContext _dbCtx;
        const byte Adr = 254;

        public CreateNodeTestsFixture(EntityTestFixture fixture)
        {
            _fixture = fixture;
            _dbCtx = fixture.DbCtx;
        }


        [Fact]
        public void create_simple_node()
        {
            _dbCtx.Nodes.Add(new Node {RfAddress = Adr, RfNetwork = 2, Signature = Int64.MaxValue, LastSeen = DateTime.Now});
            _dbCtx.SaveChanges();
            _fixture.DetachAll();
            var node = _dbCtx.Nodes.Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            _dbCtx.Nodes.Remove(node);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_Config()
        {
            _dbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                Configuration = new NodeConfiguration
                {
                    EmonCmsNodeId = null,
                    Name = "MyNode",
                    Version = "1.0"
                }
            });
            _dbCtx.SaveChanges();
            _fixture.DetachAll();
            var node = _dbCtx.Nodes.Include(_ => _.Configuration).Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            Assert.Equal("MyNode", node.Configuration.Name);
            _dbCtx.Nodes.Remove(node);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_Minimal_LatestValues()
        {
            _dbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                LatestValues = new LatestValues
                {
                }
            });
            _dbCtx.SaveChanges();
            _fixture.DetachAll();
            var node = _dbCtx.Nodes.Include(_ => _.LatestValues).Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            Assert.Null(node.LatestValues.Temperature);
            _dbCtx.Nodes.Remove(node);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_values()
        {
            var now = DateTime.UtcNow.Round(TimeSpan.FromSeconds(1));
            _dbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                LatestValues = new LatestValues
                {
                    Humidity = 12.4f,
                    Temperature = 24.2f,
                    AtmosphericPressure = 1013,
                    MaxUpTime = TimeSpan.FromMinutes(5),
                    Rssi = -78,
                    StartupTime = now,
                    VIn = 3.7f,
                    SendErrorCount = 23
                }
            });
            _dbCtx.SaveChanges();
            _fixture.DetachAll();
            var node = _dbCtx.Nodes.Include(_ => _.LatestValues).Single(_ => _.RfAddress == Adr);

            Assert.NotNull(node.LatestValues);
            Assert.Equal(12.4f, node.LatestValues.Humidity);
            Assert.Equal(24.2f, node.LatestValues.Temperature);
            Assert.Equal(1013, node.LatestValues.AtmosphericPressure);
            Assert.Equal(-78, node.LatestValues.Rssi);
            Assert.Equal(now, node.LatestValues.StartupTime);
            Assert.Equal(TimeSpan.FromMinutes(5), node.LatestValues.MaxUpTime);
            Assert.Equal(3.7f, node.LatestValues.VIn);
            Assert.Equal(23, node.LatestValues.SendErrorCount);

            _dbCtx.Nodes.Remove(node);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void create_node_with_ports()
        {
            _dbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                Ports = new List<NodePort>
                {
                    new PulsePort { Name = "Gaz", Number = 1, PulseCount = 12 },
                    new RelayPort { Name = "Hall", Number = 2 },
                    new SwitchPort { Name = "HallGround", Number = 3, State = true },
                    new VarioPort { Name = "LeavingRoom", Number = 4, Value = 24 }
                }
            });
            _dbCtx.SaveChanges();
            _fixture.DetachAll();
            var node = _dbCtx.Nodes.Include(_ => _.Ports).Single(_ => _.RfAddress == Adr);

            Assert.Contains(node.Ports.OfType<PulsePort>(), _=> _.Number == 1);
            Assert.Contains(node.Ports.OfType<RelayPort>(), _=> _.Number == 2);
            Assert.Contains(node.Ports.OfType<SwitchPort>(), _=> _.Number == 3);
            Assert.Contains(node.Ports.OfType<VarioPort>(), _=> _.Number == 4);

            _dbCtx.Nodes.Remove(node);
            _dbCtx.SaveChanges();
        }
    }
}