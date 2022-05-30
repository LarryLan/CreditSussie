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
        [TestCase("Add", 12)]
        [TestCase("Add1", 3682376528)] //int overflow test
        [TestCase("Mult", 50)]
        [TestCase("Mult1", 25)]
        [TestCase("Example", 4)]
        public void ShouldPassSimpleCases(string file, double expected)
        {
            var lines = File.ReadAllLines(@$"{file}.txt");
            var vertexDict = _converter.ConvertToVertex(lines);
            var graphVisitor = new GraphVisitor(vertexDict, _memory);
            var actual = graphVisitor.Visit();
            actual.Should().Be(expected);
        }

        [Test]
        public void ShouldPassBigTests()
        {
            var lines = File.ReadAllLines(@"input.txt");
            var vertexDict = _converter.ConvertToVertex(lines);
            var graphVisitor = new GraphVisitor(vertexDict, _memory);
            var actual = graphVisitor.Visit();
            actual.Should().Be(275225993853);
        }
    }
}
