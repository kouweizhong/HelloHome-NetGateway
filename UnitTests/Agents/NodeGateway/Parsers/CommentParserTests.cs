using System.Text;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Parsers;
using Xunit;

namespace UnitTests.Agents.NodeGateway.Parsers
{
	public class CommentParserTests
	{
		CommentParser _sut;

		public CommentParserTests()
		{
			_sut = new CommentParser();
		}


		[Fact]
		public void returns_CommentReport()
		{
			//Arrange
			var bytes = Encoding.ASCII.GetBytes("//Hello");

			//Act
			var msg = _sut.Parse(bytes);

			Assert.IsAssignableFrom<CommentReport>(msg);
		}

		[Fact]
		public void extract_comment_correctly()
		{
			//Arrange
			var bytes = Encoding.ASCII.GetBytes("//Hello");

			//Act
			var msg = (CommentReport)_sut.Parse(bytes);

			//Assert
			Assert.Equal("Hello", msg.Comment);
		}
	}
}

