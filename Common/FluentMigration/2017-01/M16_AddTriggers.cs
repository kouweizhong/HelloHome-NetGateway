using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,1,7, 22,28)]
    public class M16_AddTriggers : Migration
    {
        public override void Up()
        {
            Create.Table("Trigger")
                .WithColumn("triggerId").AsInt32().PrimaryKey().Identity()
                .WithColumn("type").AsFixedLengthAnsiString(1).NotNullable()
                .WithColumn("portGroupId").AsInt32().Nullable()
                .WithColumn("triggerOnState").AsBoolean().Nullable()
                .WithColumn("cronExpression").AsFixedLengthAnsiString(100).Nullable();
            Create.ForeignKey("FK_Trigger_PortGroup")
                .FromTable("Trigger").ForeignColumn("portGroupId")
                .ToTable("PortGroup").PrimaryColumn("portGroupId");
        }

        public override void Down()
        {
            Delete.Table("Trigger");
        }
    }
}