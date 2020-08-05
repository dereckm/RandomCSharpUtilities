using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.String
{
    public class Sequence
    {
        private readonly char[] _characters;
        private readonly Dictionary<char, int> _indexOfChar = new Dictionary<char, int>();
        private readonly HashSet<char> _uniqueCharacters = new HashSet<char>();

        public char First => _characters[0];
        public char Last => _characters[^1];

        public Sequence(IEnumerable<char> characters)
        {
            _characters = characters.ToArray();
            var index = 0;
            foreach(var c in _characters)
            {
                if(!_uniqueCharacters.Add(c))
                    throw new ArgumentException($"Duplicate character as argument is invalid: {c}");

                _indexOfChar[c] = index++;
            }
        }

        public bool Contains(char character)
        {
            return _uniqueCharacters.Contains(character);
        }

        public char GetNext(char current)
        {
            var index = _indexOfChar[current] + 1;
            index %= _characters.Length;

            return _characters[index];
        }

        public static Sequence LowerCaseSequence()
        {
            var letters = new List<char>();

            for (var i = 0; i < 26; i++)
            {
                letters.Add((char)('a' + i));
            }

            return new Sequence(letters);
        }
    }
}