using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,07,23,10,27)]
	public class CreateNodeTable : Migration
	{
		public override void Down ()
		{
			Delete.Table ("Node");
		}

		public override void Up ()
		{
			Create.Table ("Node")
				  .WithColumn ("Id").AsInt32 ().PrimaryKey ().Identity ()
				  .WithColumn ("Signature").AsCustom ("BIGINT(20) unsigned").NotNullable ()
				  .WithColumn ("RfId").AsInt16 ().NotNullable ()
				  .WithColumn ("LastSeen").AsDateTime ().NotNullable ()
				  .WithColumn ("LastRssi").AsInt32 ().NotNullable ();
		}
	}
}

