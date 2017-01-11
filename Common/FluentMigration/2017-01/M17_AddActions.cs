using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,1,8,11,29)]
    public class M17_AddActions : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Action")
                .WithColumn("actionId").AsInt32().PrimaryKey().Identity()
                .WithColumn("triggerId").AsInt32().NotNullable().ForeignKey("FK_Action_Trigger", "Trigger", "triggerId")
                .WithColumn("sequence").AsInt32().NotNullable()
                .WithColumn("type").AsAnsiString(1).NotNullable()
                .WithColumn("delayMs").AsInt32().Nullable();

            Create.Table("ActionPort")
                .WithColumn("actionId").AsInt32().ForeignKey("FK_ActionPort_Action", "Action", "actionId").PrimaryKey()
                .WithColumn("portId").AsInt32().ForeignKey("FK_ActionPort_Port", "Port", "portId").PrimaryKey();
        }
    }
}