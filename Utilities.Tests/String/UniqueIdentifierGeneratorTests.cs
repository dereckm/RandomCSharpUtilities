using System;
using FluentAssertions;
using NUnit.Framework;
using Utilities.String;

namespace Utilities.Tests.String
{
    public class UniqueIdentifierGeneratorTests
    {
        private UniqueIdentifierGenerator _systemUnderTest;

        [SetUp]
        public void SetUp()
        {
            _systemUnderTest = new UniqueIdentifierGenerator(Sequence.LowerCaseSequence());
        }

        [Test]
        public void ShouldWrapProperly()
        {
            _systemUnderTest = new UniqueIdentifierGenerator(Sequence.LowerCaseSequence(), "z");
            _systemUnderTest.GetNext().Should().Be("aa");

            _systemUnderTest = new UniqueIdentifierGenerator(Sequence.LowerCaseSequence(), "zz");
            _systemUnderTest.GetNext().Should().Be("aaa");

            _systemUnderTest = new UniqueIdentifierGenerator(Sequence.LowerCaseSequence(), "zzz");
            _systemUnderTest.GetNext().Should().Be("aaaa");
            
            // Etc...
        }

        [Test]
        public void ShouldIncrementProperly()
        {
            _systemUnderTest = new UniqueIdentifierGenerator(Sequence.LowerCaseSequence(), "a");
            _systemUnderTest.GetNext().Should().Be("b");
            _systemUnderTest.GetNext().Should().Be("c");
            _systemUnderTest.GetNext().Should().Be("d");
            _systemUnderTest.GetNext().Should().Be("e");
            _systemUnderTest.GetNext().Should().Be("f");
            _systemUnderTest.GetNext().Should().Be("g");
        }

        [Test]
        public void ShouldWorkWithCustomSequence()
        {
            var sequence = new Sequence(new [] { 'a', 'b' });
            _systemUnderTest = new UniqueIdentifierGenerator(sequence, "a");
            _systemUnderTest.GetNext().Should().Be("b");
            _systemUnderTest.GetNext().Should().Be("aa");
            _systemUnderTest.GetNext().Should().Be("ab");
            _systemUnderTest.GetNext().Should().Be("ba");
            _systemUnderTest.GetNext().Should().Be("bb");
            _systemUnderTest.GetNext().Should().Be("aaa");
        }

        [Test]
        public void ShouldThrowWhenProvidedSuffixWithCharactersOutsideSequence()
        {
            var sequence = new Sequence(new [] { 'a', 'b' });
            Func<UniqueIdentifierGenerator> createUniqueIdentifierGeneratorFunc = () => new UniqueIdentifierGenerator(sequence, "c");

            createUniqueIdentifierGeneratorFunc.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ShouldGenerateBinaryStringProperly()
        {
            var sequence = new Sequence(new [] { '0', '1' });
            
            _systemUnderTest = new UniqueIdentifierGenerator(sequence, true);

            // Jump to 8 in binary
            for (var i = 0; i < 7; i++)
            {
                _systemUnderTest.GetNext();
            }

            _systemUnderTest.GetNext().Should().Be("1000");

            // Jump to 1024 in binary
            for (var i = 0; i < 1015; i++)
            {
                _systemUnderTest.GetNext();
            }

            _systemUnderTest.GetNext().Should().Be("10000000000");
        }
    }
}