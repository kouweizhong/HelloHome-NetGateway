using System;
using System.Linq;
using NUnit.Framework;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using System.Collections.Generic;

namespace UnitTests.Logic.RfNodeIdGenerationStrategy
{
	[TestFixture]
	public class FindHoleRfNodeIdGeneratorStrategyTests
	{
		readonly FindHoleRfNodeIdGenerationStrategy _sut;

		public FindHoleRfNodeIdGeneratorStrategyTests ()
		{
			_sut = new FindHoleRfNodeIdGenerationStrategy ();
			_sut.MaxSupportedConcurrentRequest = 3;
		}

		[Test]
		public void Find_holes_when_enough_to_support_concurrent_calls()
		{
			//Arrange
			_sut.MaxSupportedConcurrentRequest = 3;

			//Act
			var foundId = _sut.FindRfAddress (new List<byte> { 1, 2, 4, 6, 8 });

			//Assert
			Assert.IsTrue(foundId < 8);
		}

		[Test]
		public void Find_something_close_DateTimeOffset_max_allocated_when_not_enough_holes() {
			//Arrange
			_sut.MaxSupportedConcurrentRequest = 3;

			//Act
			var foundId = _sut.FindRfAddress (new List<byte> { 1, 2, 3, 6, 7 });

			//Assert
			Assert.IsTrue(foundId > 7 && foundId < 10);
		}
	}
}

