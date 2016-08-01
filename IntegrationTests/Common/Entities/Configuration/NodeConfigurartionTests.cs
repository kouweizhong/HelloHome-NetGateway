using System;
using FizzWare.NBuilder;
using HelloHome.Common.Entities;
using IntegrationTests.Common.Entities.Configuration;
using NUnit.Framework;
using Xunit;

namespace IntegrationTests.Common.Entities.Configuration
{
	public class NodeConfigurartionTests 
	{
		HelloHomeDbContext ctx;

		public NodeConfigurartionTests ()
		{
			ctx = new HelloHomeDbContext ();
		}

		[Test]
		public void CanCreateEntity ()
		{
			var node = Builder<Node>.CreateNew ()
			                        .With( x=>x.Signature = Faker.RandomNumber.Next())
			                        .Build ();
			ctx.Nodes.Add (node);
			ctx.SaveChanges ();
		}

	}
}

