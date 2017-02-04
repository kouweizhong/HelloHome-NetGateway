using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration (2016, 12, 13, 10, 02)]
	public class M10_AddNodeSignature : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Alter.Table ("Node")
			     .AddColumn ("signature").AsInt64 ();
			Execute.Sql ("UPDATE Node SET signature=nodeId;");
		 	Execute.Sql ("set foreign_key_checks = 0;ALTER TABLE Node MODIFY nodeId INTEGER NOT NULL AUTO_INCREMENT;set foreign_key_checks = 1;");
		}
	}
}
