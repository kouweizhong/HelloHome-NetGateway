using System;
using System.Data;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,8,21,45)]
	public class M03_SplitLatestValues : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Create.Table ("NodeLatestValues")
				  .WithColumn ("nodeId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("vIn").AsFloat ().Nullable ()
				  .WithColumn ("sendErrorCount").AsInt32 ().NotNullable ()
				  .WithColumn ("temperature").AsFloat ().Nullable ()
				  .WithColumn ("humidity").AsFloat ().Nullable ()
				  .WithColumn ("pressure").AsInt32 ().Nullable ()
				  .WithColumn ("rssi").AsInt32 ().Nullable ()
			      .WithColumn ("startupTime").AsDateTime ().Nullable ()
				  .WithColumn ("maxUpTime").AsFloat ().Nullable ();
		    Create.ForeignKey("NodeLatestValue_Node")
		        .FromTable("NodeLatestValues")
		        .ForeignColumn("nodeId")
		        .ToTable("Node")
		        .PrimaryColumn("nodeId")
		        .OnUpdate(Rule.Cascade);
			Execute.Sql ("INSERT NodeLatestValues (nodeId, vIn, sendErrorCount, temperature, humidity, pressure, rssi, startupTime, maxUpTime) SELECT nodeId, currentVin, currentSendErrorCount, currentTemperature, currentHumidity, currentPressure, lastRssi, lastStartupTime, maxUpTime FROM Node; ");
			Delete.Column ("currentVin").FromTable ("Node");
			Delete.Column ("currentSendErrorCount").FromTable ("Node");
			Delete.Column ("currentTemperature").FromTable ("Node");
			Delete.Column ("currentHumidity").FromTable ("Node");
			Delete.Column ("currentPressure").FromTable ("Node");
			Delete.Column ("lastRssi").FromTable ("Node");
			Delete.Column ("lastStartupTime").FromTable ("Node");
			Delete.Column ("maxUpTime").FromTable ("Node");
		}
	}
}
