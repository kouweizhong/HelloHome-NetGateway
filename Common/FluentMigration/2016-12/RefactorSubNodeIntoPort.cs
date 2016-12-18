using System;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,10,19,13)]
	public class RefactorSubNodeIntoPort : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Rename.Table ("SubNode").To ("Port");
			Rename.Column ("subNodeId").OnTable ("Port").To ("portId");

			Alter.Column ("pulseCount").OnTable ("Port").AsInt32 ().Nullable ();
			Alter.Table ("Port")
				.AddColumn ("direction").AsFixedLengthAnsiString (1)
				.AddColumn ("type").AsFixedLengthAnsiString (1)
				.AddColumn ("state").AsBoolean ().Nullable ()
				.AddColumn ("value").AsInt16 ().Nullable ();

			Update.Table ("Port").Set (new { direction = "S", type = "P" }).AllRows ();

			Alter.Column ("direction").OnTable ("Port").AsFixedLengthAnsiString (1).NotNullable ();
			Alter.Column ("type").OnTable ("Port").AsFixedLengthAnsiString (1).NotNullable ();

			Create.UniqueConstraint ("UC_nodeId_number").OnTable ("Port").Columns ("nodeId", "number");
			Create.Index ("IX_nodeId_number").OnTable ("Port").OnColumn ("nodeId").Ascending ().OnColumn ("number").Ascending ();
		}
	}
}
