using System;
using FizzWare.NBuilder;
using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities.Configuration
{
	public class NodeConfigurartionTests : ConfigurationTest<Node>
	{
		protected override Node CreateEntity ()
		{
			return Builder<Node>.CreateNew ()
					.With (x => x.Signature = Faker.RandomNumber.Next ())
					.Build ();		
		}
	}
}

