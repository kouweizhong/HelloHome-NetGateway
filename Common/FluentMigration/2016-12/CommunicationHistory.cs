using System;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration (2016, 12, 10, 19, 16)]
	public class CommunicationHistory : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Create.Table ("CommunicationHistory")
				  .WithColumn ("communicationHistoryId").AsInt32 ().PrimaryKey ().Identity ()
				  .WithColumn ("type").AsFixedLengthAnsiString (1).NotNullable ()
				  .WithColumn ("extId").AsInt32 ().NotNullable ()
				  .WithColumn ("nodeId").AsInt32 ().NotNullable ()
				  .WithColumn ("time").AsDateTime ().NotNullable ()
				  .WithColumn ("rssi").AsInt16 ().NotNullable ();
			Create.ForeignKey ("CommunicationHistory_Node")
				.FromTable ("CommunicationHistory").ForeignColumn ("nodeId")
				.ToTable ("Node").PrimaryColumn ("nodeId");
			Execute.Sql ("INSERT INTO CommunicationHistory (type, extId, nodeId, time, rssi)\n\tSELECT 'E', id, nodeId, timestamp, 0 FROM EnvironmentData\n\tUNION ALL\n\tSELECT 'I', id, nodeId, timestamp, 0 FROM NodeInfoData\n\tUNION ALL\n\tSELECT 'P', id, nodeId, timestamp, 0 FROM PulseData INNER JOIN Port ON PulseData.SubNodeId = Port.portId;");

			Create.Table ("NodeHealthHistory")
				  .WithColumn ("communicationHistoryId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("vIn").AsFloat ().Nullable ()
				  .WithColumn ("sendErrorCount").AsInt16 ().NotNullable ();
			Create.ForeignKey ("NodeHealthHistory_CommunicationHostory")
				  .FromTable ("NodeHealthHistory").ForeignColumn ("communicationHistoryId")
				  .ToTable ("CommunicationHistory").PrimaryColumn ("communicationHistoryId");
			Execute.Sql ("INSERT INTO NodeHealthHistory (communicationHistoryId, vIn, sendErrorCount)\n\tSELECT communicationHistoryId, vIn, sendErrorCount FROM NodeInfoData I INNER JOIN CommunicationHistory H ON I.id=H.extId AND H.type='I';");

			Create.Table ("EnvironmentDataHistory")
				  .WithColumn ("communicationHistoryId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("temperature").AsFloat ().Nullable ()
				  .WithColumn ("humidity").AsFloat ().Nullable ()
				  .WithColumn ("pressure").AsInt16 ().Nullable ();
			Create.ForeignKey ("EnvironmentDataHistory_CommunicationHistory")
				  .FromTable ("EnvironmentDataHistory").ForeignColumn ("communicationHistoryId")
				  .ToTable ("CommunicationHistory").PrimaryColumn ("communicationHistoryId");
			Execute.Sql ("INSERT INTO EnvironmentDataHistory (communicationHistoryId, temperature, humidity, pressure)\n\tSELECT communicationHistoryId, temperature, humidity, pressure FROM EnvironmentData E INNER JOIN CommunicationHistory H ON E.id=H.extId AND H.type='E';");

			Create.Table ("PulseHistory")
				  .WithColumn ("communicationHistoryId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("portId").AsInt32 ().NotNullable ()
				  .WithColumn ("newPulses").AsInt16 ().NotNullable ()
				  .WithColumn ("total").AsInt32 ().NotNullable ()
				  .WithColumn ("isOffset").AsBoolean ().NotNullable ();
			Create.ForeignKey ("PulseHistory_CommunicationHistory")
				  .FromTable ("PulseHistory").ForeignColumn ("communicationHistoryId")
				  .ToTable ("CommunicationHistory").PrimaryColumn ("communicationHistoryId");
			Create.ForeignKey ("PulseHistory_Port")
				  .FromTable ("PulseHistory").ForeignColumn ("portId")
				  .ToTable ("Port").PrimaryColumn ("portId");
			Execute.Sql ("INSERT INTO PulseHistory (communicationHistoryId, portId, newPulses, total)\n\tSELECT communicationHistoryId, subNodeId, newPulses, newValue FROM PulseData P INNER JOIN CommunicationHistory H ON P.id=H.extId AND H.type='P';");

			Delete.Table ("EnvironmentData");
			Delete.Table ("NodeInfoData");
			Delete.Table ("PulseData");
			Delete.Column ("extId").FromTable ("CommunicationHistory");
		}

	}
}
