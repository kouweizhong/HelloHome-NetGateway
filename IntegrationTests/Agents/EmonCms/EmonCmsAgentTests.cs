using System;
using NUnit.Framework;
using NetHhGateway.Agents.EmonCms;
using NetHhGateway.Configuration.AppSettings;

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
		public void EmonCms_SimplePass ()
		{
			_sut.Send (null, new { Hello = 11.36 }, new DateTime (2015, 1, 1));
		}
	}
}

