using System.Data;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2016, 12, 19, 21, 41)]
    public class M11_AddCascadeUpdateToFk : ForwardOnlyMigration
    {
        public override void Up()
        {
            Delete.ForeignKey("nodeCOnfig_Node").OnTable("NodeConfig");
            Create.ForeignKey("FK_NodeConfig_Node")
                .FromTable("NodeConfig")
                .ForeignColumn("nodeId")
                .ToTable("Node")
                .PrimaryColumn("nodeId")
                .OnUpdate(Rule.Cascade);

            Delete.ForeignKey("FK_SubNode_Node").OnTable("Port");
            Create.ForeignKey("FK_Port_Node")
                .FromTable("Port")
                .ForeignColumn("NodeId")
                .ToTable("Node")
                .PrimaryColumn("NodeId")
                .OnUpdate(Rule.Cascade);
        }
    }
}