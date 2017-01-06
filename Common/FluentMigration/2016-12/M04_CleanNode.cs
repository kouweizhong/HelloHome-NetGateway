using System;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration (2016, 12, 10, 15, 58)]
	public class M04_CleanNode : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Alter.Table ("Node")
				 .AddColumn ("networkId").AsInt16 ().Nullable ();
			Update.Table ("Node").Set (new { networkId=256 }).AllRows ();
			Alter.Column ("networkId").OnTable ("Node").AsInt16().NotNullable ();
			Delete.Column ("validConfig").FromTable ("Node");
			Delete.Column ("upTime").FromTable ("Node");
			Delete.Column ("confHal1").FromTable ("Node");
			Delete.Column ("confHal2").FromTable ("Node");
			Delete.Column ("confDry").FromTable ("Node");
			Delete.Column ("confVinTrigger").FromTable ("Node");
			Delete.Column ("confVinMeasure").FromTable ("Node");
			Delete.Column ("confSiEnable").FromTable ("Node");
			Delete.Column ("confBmpEnable").FromTable ("Node");
			Delete.Column ("lastMsgRxTime").FromTable ("Node");

		}
	}
}
