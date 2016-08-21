using HelloHome.NetGateway.Agents.EmonCms;
using HelloHome.NetGateway.Configuration.AppSettings;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests.Agents.EmonCms
{
	public class EmonCmsAgentTests
	{
		EmonCmsAgent _sut;

		public EmonCmsAgentTests ()
		{
			_sut = new EmonCmsAgent (new AppSettingsBasedConfiguration ());	
		}

		[Fact]
		public void EmonCms_CanSendJson ()
		{
			_sut.Send (new { Tst_Hello = 11.36 });
		}

		[Fact]
		public void EmonCms_CanSendCsv ()
		{
			_sut.Send (210, new List<float> { 1, 4, 56.67f });
		}
	}
}

