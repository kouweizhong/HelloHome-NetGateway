using System.Data;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,10,22,11)]
	public class M07_AddLogs : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Create.Table ("Log")
			      .WithColumn ("logId").AsInt32 ().PrimaryKey ().Identity ()
			      .WithColumn ("nodeId").AsInt32 ().Nullable ()
			      .WithColumn ("time").AsDate ().NotNullable ()
			      .WithColumn ("type").AsFixedLengthAnsiString (4).NotNullable ()
			      .WithColumn ("data").AsAnsiString (255);
		    Create.ForeignKey("Log_Node")
		        .FromTable("Log")
		        .ForeignColumn("nodeId")
		        .ToTable("Node")
		        .PrimaryColumn("nodeId")
		        .OnUpdate(Rule.Cascade);
		}
	}
}
