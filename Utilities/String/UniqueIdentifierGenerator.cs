using System;
using System.Linq;
using System.Text;

namespace Utilities.String
{
    public class UniqueIdentifierGenerator
    {
        private readonly Sequence _sequence;
        private readonly bool _uniqueValues;
        private readonly StringBuilder _suffix;

        public UniqueIdentifierGenerator(Sequence sequence, bool uniqueValues = false)
        {
            _sequence = sequence;
            _uniqueValues = uniqueValues;
            _suffix = new StringBuilder();
        }

        public UniqueIdentifierGenerator(Sequence sequence, string startSuffix, bool uniqueValues = false)
        {
            if(startSuffix.Any(c => !sequence.Contains(c)))
                throw new ArgumentException($"Start suffix should not contain character that are not part of the sequence.");

            _sequence = sequence;
            _uniqueValues = uniqueValues;
            _suffix = new StringBuilder();
            _suffix.Append(startSuffix);
        }

        public string GetNext()
        {
            if (!IncrementNext(_suffix))
            {
                _suffix.Replace(_sequence.Last, _sequence.First);
                _suffix.Append(_sequence.First);

                if (_uniqueValues)
                    _suffix[0] = _sequence.Last;
            }

            return _suffix.ToString();
        }

        private bool IncrementNext(StringBuilder suffix)
        {
            for (var i = suffix.Length - 1; i >= 0; i--)
            {
                if (suffix[i] < _sequence.Last)
                {
                    suffix[i] = _sequence.GetNext(suffix[i]);
                    suffix.Replace(_sequence.Last, _sequence.First, i + 1, suffix.Length - 1 - i);
                    return true;
                }
            }

            return false;
        }
    }
}