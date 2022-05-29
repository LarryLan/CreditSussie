using System.Collections.Generic;
using System.ComponentModel;
using DependencyEvaluation.Model;
using FluentAssertions;
using NUnit.Framework;

namespace DependencyEvaluation.Test
{
    public class TextToVertexConverterTests
    {
        private ITextToVertexConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new TextToVertexConverter();
        }

        [Test]
        public void ShouldBeAbleToConvertValue()
        {
            var input = "10: Value -3";
            var actual = _converter.ConvertToVertex(input);
            actual.Index.Should().Be(10);
            actual.Dependents.Should().BeEmpty();
            actual.Operator.Should().Be(Operator.Value);
            actual.Value.Should().Be(-3);
            actual.Raw.Should().Be(input);
        }

        [Test]
        public void ShouldBeAbleToConvertMultiple()
        {
            var input = "1: Mult 6 2";
            var actual = _converter.ConvertToVertex(input);
            actual.Index.Should().Be(1);
            actual.Dependents.Should().BeEquivalentTo(new List<int>{6, 2});
            actual.Operator.Should().Be(Operator.Mult);
            actual.Value.Should().BeNull();
            actual.Raw.Should().Be(input);
        }

        [Test]
        public void ShouldBeAbleToConvertAdd()
        {
            var input = "1000: Add 6 2 10";
            var actual = _converter.ConvertToVertex(input);
            actual.Index.Should().Be(1000);
            actual.Dependents.Should().BeEquivalentTo(new List<int> { 6, 2, 10 });
            actual.Operator.Should().Be(Operator.Add);
            actual.Value.Should().BeNull();
            actual.Raw.Should().Be(input);
        }

        [Test]
        public void WhenInvalidOperator_ShouldThrowException()
        {
            var input = "1000: MOD 6 2 10";
            System.Action action = () => _converter.ConvertToVertex(input);
            action.Should().ThrowExactly<InvalidEnumArgumentException>();
        }

        [Test]
        public void ShouldBeAbleToConvertToDictionary()
        {
            var input1 = "999: Add 6 2 10";
            var input2 = "1000: Mult 6 2 10";
            var input3 = "1022: Value 6 2 10";

            var actual = _converter.ConvertToVertex(new string[]
            {
                input1,
                input2,
                input3
            });

            actual[999].Operator.Should().Be(Operator.Add);
            actual[1000].Operator.Should().Be(Operator.Mult);
            actual[1022].Operator.Should().Be(Operator.Value);
        }
    }
}