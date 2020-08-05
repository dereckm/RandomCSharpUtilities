using System;
using Utilities.String;

namespace Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = new Sequence(new [] { '0', '1' });
            var generator = new UniqueIdentifierGenerator(sequence);
            for (var i = 0; i < 16; i++)
            {
                Console.WriteLine(generator.GetNext());
            }

            Console.ReadLine();
        }
    }
}
