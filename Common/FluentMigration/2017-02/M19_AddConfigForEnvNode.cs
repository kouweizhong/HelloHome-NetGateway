using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,2,5,21,20)]
    public class M19_AddConfigForEnvNode : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("NodeConfig")
                .AddColumn("Sleeps").AsBoolean().NotNullable().WithDefaultValue(false)
                .AddColumn("NodeInfoFreq").AsInt16().NotNullable().WithDefaultValue(60*60)
                .AddColumn("EnvironmentFreq").AsInt16().Nullable()
                .AddColumn("RestartCheckFreq").AsInt16().NotNullable().WithDefaultValue(2*60*60)
                .AddColumn("Hal1Pin").AsInt16().Nullable()
                .AddColumn("vInTriggerPin").AsInt16().Nullable()
                .AddColumn("vInMeasurePin").AsInt16().Nullable()
                .AddColumn("Hal2Pin").AsInt16().Nullable()
                .AddColumn("Dry1Pin").AsInt16().Nullable()
                .AddColumn("Dry2Pin").AsInt16().Nullable()
                .AddColumn("SIEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
                .AddColumn("BMPEnabled").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}