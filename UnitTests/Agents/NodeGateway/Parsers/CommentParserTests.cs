using System;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using NUnit.Framework;
using System.Text;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace UnitTests.Agents.NodeGateway.Parsers
{
	[TestFixture]
	public class CommentParserTests
	{
		CommentParser _sut;

		public CommentParserTests ()
		{
			_sut = new CommentParser ();
		}

		[Test]
		public void detect_comments_based_on_two_slashes() {
			//Arrange
			var bytes = Encoding.ASCII.GetBytes ("//Hello");

			//Act
			var canParse = _sut.CanParse(bytes);

			//Assert
			Assert.IsTrue(canParse);
		}

		[Test]
		public void returns_CommentReport()
		{
			//Arrange
			var bytes = Encoding.ASCII.GetBytes ("//Hello");

			//Act
			var msg = _sut.Parse(bytes);

			Assert.IsAssignableFrom<CommentReport> (msg);
		}

		[Test]
		public void extract_comment_correctly()
		{
			//Arrange
			var bytes = Encoding.ASCII.GetBytes ("//Hello");

			//Act
			var msg = (CommentReport)_sut.Parse(bytes);

			//Assert
			Assert.AreEqual("Hello", msg.Comment);
		}
	}
}

