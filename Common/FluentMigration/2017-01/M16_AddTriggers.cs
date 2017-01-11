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
                .WithColumn("triggerOnState").AsBoolean().Nullable()
                .WithColumn("cronExpression").AsFixedLengthAnsiString(100).Nullable();
            Create.Table("TriggerPort")
                .WithColumn("triggerId").AsInt32().ForeignKey("FK_TriggerSensor_Trigger", "Trigger", "triggerId").PrimaryKey()
                .WithColumn("portId").AsInt32().ForeignKey("FK_TriggerSensor_Port", "Port", "portId").PrimaryKey();
        }

        public override void Down()
        {
            Delete.Table("Trigger");
        }
    }
}