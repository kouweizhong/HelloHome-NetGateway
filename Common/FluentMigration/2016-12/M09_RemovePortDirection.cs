using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,12,22,00)]
	public class M09_RemovePortDirection :ForwardOnlyMigration
	{
		public override void Up ()
		{
			Delete.Column ("direction").FromTable ("Port");
		}
	}
}
