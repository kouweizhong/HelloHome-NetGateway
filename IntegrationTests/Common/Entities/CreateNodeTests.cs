using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Extentions;
using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreateNodeTests : EntityTest
    {
        const byte Adr = 255;


        [Fact]
        public void create_simple_node()
        {
            DbCtx.Nodes.Add(new Node {RfAddress = Adr, RfNetwork = 2, Signature = Int64.MaxValue, LastSeen = DateTime.Now});
            DbCtx.SaveChanges();
            DetachAll();
            var node = DbCtx.Nodes.Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            DbCtx.Nodes.Remove(node);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_Config()
        {
            DbCtx.Nodes.Add(new Node
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
            DbCtx.SaveChanges();
            DetachAll();
            var node = DbCtx.Nodes.Include(_ => _.Configuration).Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            Assert.Equal("MyNode", node.Configuration.Name);
            DbCtx.Nodes.Remove(node);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_Minimal_LatestValues()
        {
            DbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                LatestValues = new LatestValues
                {
                }
            });
            DbCtx.SaveChanges();
            DetachAll();
            var node = DbCtx.Nodes.Include(_ => _.LatestValues).Single(_ => _.RfAddress == Adr);

            Assert.Equal(Adr, node.RfAddress);
            Assert.Null(node.LatestValues.Temperature);
            DbCtx.Nodes.Remove(node);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void Create_Node_With_values()
        {
            var now = DateTime.UtcNow.Round(TimeSpan.FromSeconds(1));
            DbCtx.Nodes.Add(new Node
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
            DbCtx.SaveChanges();
            DetachAll();
            var node = DbCtx.Nodes.Include(_ => _.LatestValues).Single(_ => _.RfAddress == Adr);

            Assert.NotNull(node.LatestValues);
            Assert.Equal(12.4f, node.LatestValues.Humidity);
            Assert.Equal(24.2f, node.LatestValues.Temperature);
            Assert.Equal(1013, node.LatestValues.AtmosphericPressure);
            Assert.Equal(-78, node.LatestValues.Rssi);
            Assert.Equal(now, node.LatestValues.StartupTime);
            Assert.Equal(TimeSpan.FromMinutes(5), node.LatestValues.MaxUpTime);
            Assert.Equal(3.7f, node.LatestValues.VIn);
            Assert.Equal(23, node.LatestValues.SendErrorCount);

            DbCtx.Nodes.Remove(node);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void create_node_with_ports()
        {
            DbCtx.Nodes.Add(new Node
            {
                RfAddress = Adr,
                RfNetwork = 2,
                Signature = Int64.MaxValue,
                LastSeen = DateTime.Now,
                Ports = new List<Port>
                {
                    new PulseSensor { Name = "Gaz", Number = 1, PulseCount = 12 },
                    new RelayActuator { Name = "Hall", Number = 2 },
                    new SwitchSensor { Name = "HallGround", Number = 3, State = true },
                    new VarioSensor { Name = "LeavingRoom", Number = 4, Value = 24 }
                }
            });
            DbCtx.SaveChanges();
            DetachAll();
            var node = DbCtx.Nodes.Include(_ => _.Ports).Single(_ => _.RfAddress == Adr);

            Assert.Contains(node.Ports.OfType<PulseSensor>(), _=> _.Number == 1);
            Assert.Contains(node.Ports.OfType<RelayActuator>(), _=> _.Number == 2);
            Assert.Contains(node.Ports.OfType<SwitchSensor>(), _=> _.Number == 3);
            Assert.Contains(node.Ports.OfType<VarioSensor>(), _=> _.Number == 4);

            DbCtx.Nodes.Remove(node);
            DbCtx.SaveChanges();
        }
    }
}