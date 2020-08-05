using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Utilities.String;

namespace Utilities.Tests.String
{
    public class SequenceTests
    {
        [Test]
        public void GetsNextCharProperly()
        {
            var sequence = new Sequence(new[] { 'a', 'b', 'c' });

            sequence.GetNext('a').Should().Be('b');
            sequence.GetNext('b').Should().Be('c');
            sequence.GetNext('c').Should().Be('a');
        }

        [Test]
        public void ContainsExpectedChar()
        {
            var sequence = new Sequence(new[] { 'a', 'b', 'c' });
            sequence.Contains('a').Should().BeTrue();
            sequence.Contains('b').Should().BeTrue();
            sequence.Contains('c').Should().BeTrue();
        }

        [Test]
        public void ShouldThrowWhenDuplicateCharacters()
        {
            Func<Sequence> createSequenceFunc = () => new Sequence(new [] { 'a', 'a', 'c' });

            createSequenceFunc.Should().Throw<ArgumentException>();
        }
    }
}