using NUnit.Framework;
using HelloHome.NetGateway.Agents.EmonCms;
using HelloHome.NetGateway.Configuration.AppSettings;
using System.Collections.Generic;

namespace IntegrationTests.Agents.EmonCms
{
	[TestFixture]
	public class EmonCmsAgentTests
	{
		EmonCmsAgent _sut;

		public EmonCmsAgentTests ()
		{
			_sut = new EmonCmsAgent (new AppSettingsBasedConfiguration ());	
		}

		[Test]
		public void EmonCms_CanSendJson ()
		{
			_sut.Send (new { Tst_Hello = 11.36 });
		}

		[Test]
		public void EmonCms_CanSendCsv ()
		{
			_sut.Send (210, new List<float> { 1, 4, 56.67f });
		}
	}
}

