using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using System.Text;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
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
		public void detect_comments_based_on_two_slashes()
		{
			//Arrange
			var bytes = Encoding.ASCII.GetBytes("//Hello");

			//Act
			var canParse = _sut.CanParse(bytes);

			//Assert
			Assert.True(canParse);
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

