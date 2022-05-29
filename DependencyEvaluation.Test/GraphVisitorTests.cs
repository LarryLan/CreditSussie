using System.IO;
using System.Linq;
using DependencyEvaluation.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace DependencyEvaluation.Test
{
    public class MemoryTests
    {
        private ITextToVertexConverter _converter;
        private Mock<IMemory> _memoryMock;

        [SetUp]
        public void Setup()
        {
            _converter = new TextToVertexConverter();
            _memoryMock = new Mock<IMemory>();
        }

        [Test]
        public void WhenIndexIsEvaluatedMultipleTimes_ShouldGetFromMemory()
        {
            var lines = File.ReadAllLines(@"TestCases/MemTest.txt");
            var vertexDict = _converter.ConvertToVertex(lines);
            double expected = 2;
            _memoryMock.Setup(x => x.TryGetByIndex(3, out expected)).Returns(true);
            var graphVisitor = new GraphVisitor(vertexDict, _memoryMock.Object);
            var actual = graphVisitor.Visit();
            actual.Should().Be(expected);
            _memoryMock.Verify(x => x.Insert(It.IsAny<int>(), It.IsAny<double>()), Times.Never);
        }
    }
}
