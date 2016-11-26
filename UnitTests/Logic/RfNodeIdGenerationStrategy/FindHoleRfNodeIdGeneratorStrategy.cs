using HelloHome.NetGateway.Commands.RfNodeIdGenerationStrategy;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Logic.RfNodeIdGenerationStrategy
{
	public class FindHoleRfNodeIdGeneratorStrategyTests
	{
		readonly FindHoleRfIdGenerationStrategy _sut;

		public FindHoleRfNodeIdGeneratorStrategyTests()
		{
			_sut = new FindHoleRfIdGenerationStrategy();
			_sut.MaxSupportedConcurrentRequest = 3;
		}

		[Fact]
		public void Find_holes_when_enough_to_support_concurrent_calls()
		{
			//Arrange
			_sut.MaxSupportedConcurrentRequest = 3;

			//Act
			var foundId = _sut.FindRfAddress(new List<byte> { 1, 2, 4, 6, 8 });

			//Assert
			Assert.True(foundId < 8);
		}

		[Fact]
		public void Find_something_close_max_allocated_when_not_enough_holes()
		{
			//Arrange
			_sut.MaxSupportedConcurrentRequest = 3;

			//Act
			var foundId = _sut.FindRfAddress(new List<byte> { 1, 2, 3, 6, 7 });

			//Assert
			Assert.True(foundId > 7 && foundId < 10);
		}
	}
}

