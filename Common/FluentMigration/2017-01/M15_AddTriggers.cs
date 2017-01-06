using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,1,5, 22,26)]
    public class M15_AddTriggers : Migration
    {
        public override void Up()
        {
            Create.Table("PortGroup")
                .WithColumn("portGroupId").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsAnsiString(255).NotNullable();

            Create.Table("PortGroupPort")
                .WithColumn("portGroupPortId").AsInt32().PrimaryKey().Identity()
                .WithColumn("portGroupId").AsInt32().NotNullable()
                .WithColumn("portId").AsInt32().NotNullable();
            Create.ForeignKey("FK_PortGroupPort_PortGroup")
                .FromTable("PortGroupPort").ForeignColumn("portGroupId")
                .ToTable("PortGroup").PrimaryColumn("portGroupId");
            Create.ForeignKey("FK_PortGroupPort_Port")
                .FromTable("PortGroupPort").ForeignColumn("portId")
                .ToTable("Port").PrimaryColumn("portId");

            Create.Table("Trigger")
                .WithColumn("triggerId").AsInt32().PrimaryKey().Identity()
                .WithColumn("type").AsFixedLengthAnsiString(1).NotNullable()
                .WithColumn("portGroupId").AsInt32().Nullable()
                .WithColumn("triggerOnState").AsBoolean().Nullable()
                .WithColumn("cronExpression").AsFixedLengthAnsiString(100).Nullable()
                .WithColumn("delayMs").AsInt32().Nullable();
            Create.ForeignKey("FK_Trigger_PortGroup")
                .FromTable("Trigger").ForeignColumn("portGroupId")
                .ToTable("PortGroup").PrimaryColumn("portGroupId");
        }

        public override void Down()
        {
            Delete.Table("Trigger");

            Delete.ForeignKey("FK_PortGroupPort_PortGroup").OnTable("PortGroupPort");
            Delete.ForeignKey("FK_PortGroupPort_Port").OnTable("PortGroupPort");
            Delete.Table("PortGroupPort");

            Delete.Table("PortGroup");
        }
    }
}