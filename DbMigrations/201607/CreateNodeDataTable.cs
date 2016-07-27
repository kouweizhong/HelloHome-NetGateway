using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,7,24,19,47)]
	public class CreateNodeDataTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_NodeData_Node").OnTable ("NodeData");
			Delete.Table ("NodeData");
		}

		public override void Up ()
		{
			Create.Table ("Nodedata")
			      .WithColumn ("Id").AsInt32 ().PrimaryKey ().Identity()
				  .WithColumn ("NodeId").AsInt32 ().NotNullable ()
				  .WithColumn ("Date").AsDateTime ().NotNullable ()
				  .WithColumn ("VIn").AsFloat ().Nullable ()
				  .WithColumn ("ErrCount").AsInt32 ().NotNullable ()
				  .WithColumn ("UpTime").AsFloat ().NotNullable ();
			Create.ForeignKey ("FK_NodeData_Node")
				  .FromTable ("NodeData").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("ID");
		}
	}
}

