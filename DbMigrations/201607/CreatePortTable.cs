using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,7,24,19,51)]
	public class CreatePortTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_Port_Node").OnTable ("Port");
			Delete.Table ("Port");
		}

		public override void Up ()
		{
			Create.Table ("Port")
			      .WithColumn ("Id").AsInt32 ().PrimaryKey ().Identity()
				  .WithColumn ("NodeId").AsInt32 ().NotNullable ()
				  .WithColumn ("Name").AsString (50).Nullable ()
				  .WithColumn ("Number").AsInt16 ().NotNullable ()
				  .WithColumn ("Type").AsFixedLengthString (1).NotNullable ()
				  .WithColumn ("Relay_State").AsBoolean ().Nullable ()
				  .WithColumn ("Relay_StateConfirmed").AsBoolean ().Nullable ();
			Create.ForeignKey ("FK_Port_Node")
				  .FromTable ("Port").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}

