using System.Collections.Generic;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Queries;
using Moq;
using Xunit;

namespace UnitTests.Logic.RfNodeIdGenerationStrategy
{
	public class FindHoleRfNodeIdGeneratorStrategyTests
	{
		readonly FindHoleRfIdGenerationStrategy _sut;
	    private readonly Mock<IListRfIdsQuery> _findExisiting;

	    public FindHoleRfNodeIdGeneratorStrategyTests()
		{
		    _findExisiting = new Mock<IListRfIdsQuery>();
			_sut = new FindHoleRfIdGenerationStrategy(_findExisiting.Object);
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

