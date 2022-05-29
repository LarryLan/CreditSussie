using System.IO;
using System.Linq;
using DependencyEvaluation.Model;
using FluentAssertions;
using NUnit.Framework;

namespace DependencyEvaluation.Test
{
    public class GraphVisitorTests
    {
        private ITextToVertexConverter _converter;
        private IMemory _memory;

        [SetUp]
        public void Setup()
        {
            _converter = new TextToVertexConverter();
            _memory = new Memory();
        }

        [Test]
        public void ShouldPassTestInExample()
        {
            var lines = File.ReadAllLines(@"TestCases/Example.txt");
            var vertexDict = _converter.ConvertToVertex(lines);
            var graphVisitor = new GraphVisitor(vertexDict, _memory);
            var actual = graphVisitor.Visit();
            actual.Should().Be(4);
        }

        [Test]
        public void ShouldPassBigTests()
        {
            var lines = File.ReadAllLines(@"TestCases/input.txt");
            var vertexDict = _converter.ConvertToVertex(lines);
            var graphVisitor = new GraphVisitor(vertexDict, _memory);
            var actual = graphVisitor.Visit();
            actual.Should().Be(275225993853);
        }
    }
}
